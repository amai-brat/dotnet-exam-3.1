using System.Security.Claims;
using Generic.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.RatingService.Extensions;
using TicTacToe.RatingService.UseCases.Rating.Queries.GetGlobalRating;
using TicTacToe.RatingService.UseCases.Rating.Queries.GetPersonalRating;

namespace TicTacToe.RatingService.Controllers;

[Route("")]
[ApiController]
public class RatingController(IMediator mediator): ControllerBase
{
    [HttpGet("global")]
    public async Task<IActionResult> GetGlobalRating()
    {
        var result = await mediator.Send(new GetGlobalRatingQuery());
        
        return result.IsFailed ? 
            this.ErrorResult(result.Errors.First()) : 
            Ok();
    }
    
    [HttpGet("personal")]
    [Authorize]
    public async Task<IActionResult> GetPersonalRating()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await mediator.Send(new GetPersonalRatingQuery(){UserId = userId});
        
        return result.IsFailed ? 
            this.ErrorResult(result.Errors.First()) : 
            Ok();
    }
}