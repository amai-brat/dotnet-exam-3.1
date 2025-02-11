using Generic.Mediator;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.MainService.Application.Features.Games.Commands.CreateGame;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;

namespace TicTacToe.MainService.Controllers;

[ApiController]
[Route("games")]
public class GameController(
    IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGames([FromQuery] int count, [FromQuery] int page)
    {
        var result = await mediator.Send(new GetGamesQuery(count, page));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}