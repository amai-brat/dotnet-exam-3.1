using TicTacToe.RatingService.Abstractions.Repositories;
using TicTacToe.RatingService.DataAccess;
using TicTacToe.RatingService.DataAccess.Repositories;
using TicTacToe.RatingService.Options;

namespace TicTacToe.RatingService.Extensions;

public static class AddDbContextExtension
{
    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<DbOptions>(configuration.GetSection("Database"));

        serviceCollection.AddScoped<IRatingRepository, RatingRepository>();
        serviceCollection.AddScoped<AppDbContext>();
        
        return serviceCollection;
    }
}