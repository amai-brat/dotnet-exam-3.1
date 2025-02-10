using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Infrastructure.Data;
using TicTacToe.MainService.Infrastructure.Data.Repositories;
using TicTacToe.MainService.Infrastructure.Profiles;

namespace TicTacToe.MainService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        
        services.AddAutoMapper(conf =>
        {
            conf.AddProfile<UserProfile>();
            conf.AddProfile<GameProfile>();
        });
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}