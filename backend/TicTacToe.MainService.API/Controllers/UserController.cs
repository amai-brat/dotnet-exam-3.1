using Generic.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.MainService.Application.Features.Users.Queries.GetPersonalInfo;

namespace TicTacToe.MainService.Controllers;

[ApiController]
[Route("")]
public class UserController(
    IMediator mediator
    ) : ControllerBase
{
    [Authorize]
    [Route("personal")]
    public async Task<IActionResult> GetPersonalInfoAsync()
    {
        var result = await mediator.Send(new GetPersonalInfoQuery());
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result.Value);
    }
}