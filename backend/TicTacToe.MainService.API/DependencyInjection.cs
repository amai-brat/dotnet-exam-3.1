using MassTransit;
using TicTacToe.MainService.Consumers;

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
}