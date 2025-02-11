using System.Collections.Concurrent;
using System.Text.Json;
using Generic.Mediator;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGame;
using TicTacToe.MainService.Application.Services;
using TicTacToe.MainService.Consts;
using TicTacToe.MainService.Domain.Entities;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.MainService.Hubs;

// TODO: [Authorize]
public class GameHub(
    [FromKeyedServices(KeyedServices.GameRooms)] ConcurrentDictionary<int, Room> rooms,
    IBus bus,
    IMediator mediator,
    ITicTacToeGame ticTacToeGame,
    ILogger<GameHub> logger
    ) : Hub
{
    private readonly TimeSpan _gameRefreshInterval = TimeSpan.FromSeconds(5);
    private readonly ConcurrentDictionary<int, Room> _rooms = rooms;

    public async Task JoinRoom(int gameId)
    {
        var result = await mediator.Send(new GetGameQuery(gameId));
        if (result.IsFailed)
        {
            await SendErrorToCaller(result.Errors.Select(x => x.Message).ToList());
        }
        if (result.Value.Game.Status == GameStatus.Closed)
        {
            await SendErrorToCaller(["Game is closed"]);
        }
        
        var room = _rooms.GetOrAdd(gameId, _ => new Room
        {
            Id = gameId,
            Connections = [],
            IsStarted = false,
            MaxRating = result.Value.Game.MaxRating
        });
        
        room.Connections.Add(Context.ConnectionId);
        
        for (var i = 0; i < room.Connections.Count; i++)
        {
            Console.WriteLine($"room: {room.Id}: {i}: {room.Connections[i]}");
        }

        if (room.IsStarted)
        {
            await Clients.Caller.SendAsync("GameStarted", true);
        }
    }
    
    public async Task JoinGame(int gameId)
    {
        var result = await mediator.Send(new GetGameQuery(gameId));
        if (result.IsFailed)
        {
            await SendErrorToCaller(result.Errors.Select(x => x.Message).ToList());
        }
        if (result.Value.Game.Status == GameStatus.Closed)
        {
            await SendErrorToCaller(["Game is closed"]);
        }

        var room = _rooms.GetOrAdd(gameId, _ => new Room
        {
            Id = gameId,
            Connections = [],
            MaxRating = result.Value.Game.MaxRating
        });

        if (room.Player1 is not null && room.Player2 is not null) return;

        // TODO: auth
        if (room.Player1 is null)
        {
            room.Player1 = new UserInfo(Context.ConnectionId, 0, "Player 1");
            logger.LogInformation("Player 1 of room {RoomId} - {ConnectionId}", gameId, Context.ConnectionId);
        } 
        else if (room.Player2 is null)
        {
            room.Player2 = new UserInfo(Context.ConnectionId, 0, "Player 2");
            logger.LogInformation("Player 2 of room {RoomId} - {ConnectionId}", gameId, Context.ConnectionId);
        }

        if (room.Player1 is not null && room.Player2 is not null)
        {
            room.Turn = Turn.FirstPlayer;
            room.IsStarted = true;
            await Clients.Clients(room.Connections).SendAsync("GameStarted", true);
            await Clients.Client(room.Player1!.ConnectionId).SendAsync("Turn");
        }
        
        // switch (room.Connections.Count)
        // {
        //    
        //     case 1:
        //         room.Player1 = new UserInfo(Context.ConnectionId, 0, "Player 1");
        //         logger.LogInformation("Player 1 of room {RoomId} - {ConnectionId}", gameId, Context.ConnectionId);
        //         break;
        //     case 2:
        //         room.Turn = Turn.FirstPlayer;
        //         await Clients.Client(room.Player1!.ConnectionId).SendAsync("Turn");
        //         
        //         room.Player2 = new UserInfo(Context.ConnectionId, 0, "Player 2");
        //         logger.LogInformation("Player 2 of room {RoomId} - {ConnectionId}", gameId, Context.ConnectionId);
        //         break;
        //     default:
        //         await SendRefreshGridToRoom(room);
        //         break;
        // }
    }

    public async Task SendTurn(int idx, int jdx)
    {
        var room = _rooms.Values.SingleOrDefault(x => x.Connections.Contains(Context.ConnectionId));
        if (room == null)
        {
            logger.LogInformation("User {ConnectionId} sent turn to room but he's without room", Context.ConnectionId);
            return;
        }
        
        logger.LogInformation("Player {ConnectionId} (Room {RoomId}) sent turn on {Idx}, {Jdx}", Context.ConnectionId, room.Id, idx, jdx);

        room.Grid[idx, jdx] = room.Turn switch
        {
            Turn.FirstPlayer when room.Player1?.ConnectionId == Context.ConnectionId => 1,
            Turn.SecondPlayer when room.Player2?.ConnectionId == Context.ConnectionId => 2,
            _ => room.Grid[idx, jdx]
        };

        var result = ticTacToeGame.GetGameResult(room.Grid);
        if (result.IsFailed)
        {
            await SendErrorToCaller(result.Errors.Select(x => x.Message).ToList());
        }

        await SendRefreshGridToRoom(room);
        if (result.Value == GameResult.Continue)
        {
            if (room.Turn == Turn.FirstPlayer)
            {
                room.Turn = Turn.SecondPlayer;
                await Clients.Client(room.Player2!.ConnectionId).SendAsync("Turn");
            }
            else
            {
                room.Turn = Turn.FirstPlayer;
                await Clients.Client(room.Player1!.ConnectionId).SendAsync("Turn");
            }
            
            return;
        }
        
        
        var ratingPoints = ticTacToeGame.GetRatingPoints(result.Value, 
            room.Player1!.UserId, 
            room.Player2!.UserId);
        await bus.Publish(ratingPoints.Value);
        await Clients.Clients(room.Connections)
            .SendAsync("ResultAnnouncement", 
                GetResultAnnouncementMessage(result.Value, room.Player1, room.Player2));

        await Task.Delay(_gameRefreshInterval);
        room.ResetGrid();
        room.Turn = Turn.FirstPlayer;
        await SendRefreshGridToRoom(room);
        await Clients.Client(room.Player1!.ConnectionId).SendAsync("Turn");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var room = _rooms.Values.SingleOrDefault(x => x.Connections.Contains(Context.ConnectionId));
        if (room == null)
        {
            await base.OnDisconnectedAsync(exception);
            return;
        }
        
        logger.LogInformation("Player {ConnectionId} in room {RoomId} disconnected", Context.ConnectionId, room.Id);

        room.Connections.Remove(Context.ConnectionId);
        
        if (room.Player1?.ConnectionId == Context.ConnectionId)
        {
            room.Player1 = room.Player2;
            room.Player2 = null;
            room.ResetGrid();
            room.IsStarted = false;
            await Clients.Clients(room.Connections).SendAsync("GameStarted", false);
            await SendRefreshGridToRoom(room);
        }
        
        if (room.Player2?.ConnectionId == Context.ConnectionId)
        {
            room.Player2 = null;
            room.ResetGrid();
            room.IsStarted = false;
            await Clients.Clients(room.Connections).SendAsync("GameStarted", false);
            await SendRefreshGridToRoom(room);
        }
        
        await base.OnDisconnectedAsync(exception);
    }

    private async Task SendErrorToCaller(List<string> errors)
    {
        await Clients.Caller.SendAsync("Error", errors);
    }

    private async Task SendRefreshGridToRoom(Room room)
    {
        var rows = room.Grid.GetLength(0);
        var cols = room.Grid.GetLength(1);
        var res = new int[rows][];
        for (int i = 0; i < rows; i++)
        {
            res[i] = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                res[i][j] = room.Grid[i, j];
            }
        }
        await Clients.Clients(room.Connections).SendAsync("RefreshGrid", res);
    }

    private string GetResultAnnouncementMessage(GameResult result, UserInfo firstPlayer, UserInfo secondPlayer)
    {
        return result switch
        {
            GameResult.Draw => "Draw",
            GameResult.FirstPlayerWon => $"{firstPlayer.Username} won, {secondPlayer.Username} lost",
            GameResult.SecondPlayerWon => $"{secondPlayer.Username} won, {firstPlayer.Username} lost",
            GameResult.Continue => throw new InvalidOperationException("Game is not finished"),
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
        };
    }
}

public record UserInfo(string ConnectionId, int UserId, string Username);
public class Room
{
    public int Id { get; set; }
    public List<string> Connections { get; set; } = [];
    public UserInfo? Player1 { get; set; }
    public UserInfo? Player2 { get; set; }

    public bool IsStarted { get; set; }
    public int MaxRating { get; set; }
    public Turn Turn { get; set; }

    public int[,] Grid { get; private set; } =
    {
        { 0, 0, 0 }, 
        { 0, 0, 0 }, 
        { 0, 0, 0 }
    };

    public void ResetGrid()
    {
        Grid = new int[3, 3];
    }
}

public enum Turn
{
    FirstPlayer = 1,
    SecondPlayer = 2
}