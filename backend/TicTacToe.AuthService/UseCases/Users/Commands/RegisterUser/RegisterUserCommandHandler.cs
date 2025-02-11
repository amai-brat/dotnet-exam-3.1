using FluentResults;
using Generic.Mediator;
using TicTacToe.AuthService.Abstractions.Repositories;
using TicTacToe.AuthService.Abstractions.Services;
using TicTacToe.AuthService.Entities;

namespace TicTacToe.AuthService.UseCases.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IHasherService hasherService): IRequestHandler<RegisterUserCommand, Result>
{
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!ValidateUser(request.User, out var error))
        {
            return Result.Fail(error);
        }

        var dbUser = await userRepository.GetEntityByFilterAsync(u => u.Login == request.User.Login);
        if (dbUser is not null)
        {
            return Result.Fail(new RegisterError(RegisterError.LoginAlreadyExist));
        }

        await userRepository.InsertEntityAsync(new User()
        {
            Login = request.User.Login,
            Password = hasherService.GetHash(request.User.Password)
        });
        
        return Result.Ok();
    }

    private bool ValidateUser(UserRegisterDto user, out string? error)
    {
        error = null;
        return true;
    }
}