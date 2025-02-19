using MassTransit;
using TicTacToe.RatingService.Consumers;
using TicTacToe.RatingService.Options;

namespace TicTacToe.RatingService.Extensions;

public static class AddMasstransitExtension
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection serviceCollection, RabbitMqOptions options)
    {
        serviceCollection.AddMassTransit(x =>
        {
            x.AddConsumer<MatchEndedConsumer>();
            x.AddConsumer<UserRegisteredConsumer>();
            // x.SetKebabCaseEndpointNameFormatter();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.Host(options.Hostname, 5672, "/", h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
            });
        });

        return serviceCollection;
    }
}