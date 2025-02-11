using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Application.Features.Games.Queries.GetGames;

public class GetGamesDto
{
    public List<GameDto> Games { get; set; } = null!;
}

public class GameDto
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public UserDto CreatedBy { get; set; } = null!;
    public int MaxRating { get; set; }

    public GameStatus Status { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
}