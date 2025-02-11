using TicTacToe.RatingService.Abstractions.Consumers;
using TicTacToe.RatingService.Consumers;

namespace TicTacToe.RatingService.Extensions;

public static class AddConsumersExtensions
{
    public static IServiceCollection AddConsumers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMatchEndedConsumer, MatchEndedConsumer>();
        serviceCollection.AddScoped<IUserRegisteredConsumer, UserRegisteredConsumer>();
        
        return serviceCollection;
    }
}