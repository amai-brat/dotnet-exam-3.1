using MassTransit;
using TicTacToe.AuthService.Options;

namespace TicTacToe.AuthService.Extensions;

public static class AddMasstransitExtension
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection serviceCollection, RabbitMqOptions options)
    {
        serviceCollection.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.Host(options.Hostname, 15672, "/", h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
            });
        });

        return serviceCollection;
    }
}