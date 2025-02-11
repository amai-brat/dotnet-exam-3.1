using TicTacToe.AuthService.Abstractions.Services;
using TicTacToe.AuthService.Options;
using TicTacToe.AuthService.Services;

namespace TicTacToe.AuthService.Extensions;

public static class AddServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        serviceCollection.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        serviceCollection.AddSingleton<IJwtService, JwtService>();
        
        serviceCollection.AddSingleton<IHasherService, HasherService>();
        
        return serviceCollection;
    }
}