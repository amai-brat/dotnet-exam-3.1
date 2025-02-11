using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;

namespace TicTacToe.MainService.Application.Features.Games.Queries.GetGame;

public class GetGameDto
{
    public GameDto Game { get; set; } = null!;
}