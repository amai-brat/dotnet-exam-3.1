using FluentValidation;
using TicTacToe.AuthService.UseCases.Users.Commands.LoginUser;
using TicTacToe.AuthService.UseCases.Users.Commands.RegisterUser;

namespace TicTacToe.AuthService.Extensions;

public static class AddValidatorsExtension
{
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IValidator<UserLoginDto>, UserLoginDtoValidator>();
        serviceCollection.AddTransient<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
        
        return serviceCollection;
    }
}