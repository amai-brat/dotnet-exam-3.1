using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TicTacToe.MainService.Application.Exceptions;

namespace TicTacToe.MainService.Application.Helpers;

public static class HttpContextExtensions
{
    public static int FindUserId(this HttpContext httpContext)
    {
        var claim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is null)
        {
            throw new UnauthorizedException("User is not logged in");
        }
        return int.Parse(claim.Value);
    }
}