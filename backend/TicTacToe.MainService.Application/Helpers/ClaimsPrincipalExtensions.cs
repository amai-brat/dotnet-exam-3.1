using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TicTacToe.MainService.Application.Exceptions;

namespace TicTacToe.MainService.Application.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static int FindUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        if (claim is null)
        {
            throw new UnauthorizedException("User is not logged in");
        }
        return int.Parse(claim.Value);
    }

    public static string FindUsername(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.FindFirst(ClaimTypes.Name);
        if (claim is null)
        {
            throw new UnauthorizedException("User is not logged in");
        }
        return claim.Value;
    }
}