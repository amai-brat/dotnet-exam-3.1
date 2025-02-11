using Generic.Mediator.DependencyInjectionExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.MainService.Application.Helpers;
using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Application.Services;
using TicTacToe.MainService.Infrastructure.Data;
using TicTacToe.MainService.Infrastructure.Data.Repositories;
using TicTacToe.MainService.Infrastructure.Profiles;

namespace TicTacToe.MainService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITicTacToeGame, TicTacToeGame>();
        
        services.AddRepositories();
        services.AddMediator(AssemblyReference.Assembly);
        services.AddHttpContextAccessor();

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