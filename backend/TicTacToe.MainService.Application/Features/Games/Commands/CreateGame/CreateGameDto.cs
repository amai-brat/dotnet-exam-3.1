using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;

namespace TicTacToe.MainService.Application.Features.Games.Commands.CreateGame;

public class CreateGameDto
{
    public GameDto Game { get; set; } = null!;
}