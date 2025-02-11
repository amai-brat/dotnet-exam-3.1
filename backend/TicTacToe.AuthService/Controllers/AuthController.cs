using Generic.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.AuthService.Extensions;
using TicTacToe.AuthService.UseCases.Users.Commands.LoginUser;
using TicTacToe.AuthService.UseCases.Users.Commands.RegisterUser;

namespace TicTacToe.AuthService.Controllers;

[Route("")]
[ApiController]
public class AuthController(IMediator mediator): ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto user)
    {
        var result = await mediator.Send(new RegisterUserCommand(){User = user});
        
        return result.IsFailed ? 
            this.ErrorResult(result.Errors.First()) : 
            Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto user)
    {
        var result = await mediator.Send(new LoginUserCommand(){User = user});
        
        if(result.IsFailed)
            return this.ErrorResult(result.Errors.First());
        
        SetAuthCookie(result.Value);
        return Ok();
    }
    
    [HttpPost("signout")]
    public IActionResult SignoutAsync()
    {
        HttpContext.Response.Cookies.Append("Jwt", string.Empty, new CookieOptions
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddHours(-2)
        });
        
        return Ok();
    }
    
    [NonAction]
    private void SetAuthCookie(LoginResultDto loginResult)
    {
        HttpContext.Response.Cookies.Append("Jwt", loginResult.JwtToken, new CookieOptions()
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddHours(2)
        });
    }
}