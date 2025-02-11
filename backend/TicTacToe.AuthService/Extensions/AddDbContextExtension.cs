using Microsoft.EntityFrameworkCore;
using TicTacToe.AuthService.Abstractions;
using TicTacToe.AuthService.Abstractions.Repositories;
using TicTacToe.AuthService.DataAccess;
using TicTacToe.AuthService.DataAccess.Repositories;
using TicTacToe.AuthService.Options;

namespace TicTacToe.AuthService.Extensions;

public static class AddDbContextExtension
{
    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection, DbOptions options)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        
        return serviceCollection.AddDbContext<AppDbContext>(builder =>
        {
            builder.UseNpgsql(options.ConnectionString);
            builder.UseSnakeCaseNamingConvention();
        });
    }
}