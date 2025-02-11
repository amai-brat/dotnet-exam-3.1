using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.MainService.Consumers;
using TicTacToe.MainService.Options;

namespace TicTacToe.MainService;

public static class DependencyInjection
{
    public static IServiceCollection AddMassTransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("RabbitMq") 
                      ?? throw new NullReferenceException("RabbitMq configuration is null");
        
        services.AddMassTransit(conf =>
        {
            conf.SetKebabCaseEndpointNameFormatter();
            
            conf.AddConsumer<UserRegisteredConsumer>();
            
            conf.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(section["Host"] ?? throw new NullReferenceException("RabbitMq:Host is null"), 
                    h =>
                {
                    h.Username(section["Username"] ?? throw new NullReferenceException("RabbitMq:Username is null"));
                    h.Password(section["Password"] ?? throw new NullReferenceException("RabbitMq:Password is null"));
                });
                
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, params string[] origins)
    {
        services.AddCors(options =>
            options.AddPolicy("SPA", builder =>
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(origins)
                        .AllowCredentials()));

        return services;
    }
    
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