using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.RatingService.Options;

namespace TicTacToe.RatingService.Extensions;

public static class AddJwtAuthenticationExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection serviceCollection, JwtOptions jwtOptions)
    {
        serviceCollection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var request = context.HttpContext.Request;
                        var cookies = request.Cookies;
                        if (cookies.TryGetValue("Jwt",
                                out var accessTokenValue))
                        {
                            request.Headers.Append("Authorization", $"Bearer {accessTokenValue}");
                        }
                        return Task.CompletedTask;
                    },
                };
            });

        return serviceCollection;
    }
}