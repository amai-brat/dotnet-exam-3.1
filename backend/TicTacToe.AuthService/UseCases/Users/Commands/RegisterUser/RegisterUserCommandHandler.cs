using FluentResults;
using Generic.Mediator;
using MassTransit;
using TicTacToe.AuthService.Abstractions.Repositories;
using TicTacToe.AuthService.Abstractions.Services;
using TicTacToe.AuthService.Entities;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.AuthService.UseCases.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IHasherService hasherService,
    IBus bus): IRequestHandler<RegisterUserCommand, Result>
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

        var user = new User()
        {
            Login = request.User.Login,
            Password = hasherService.GetHash(request.User.Password)
        };
        
        await userRepository.InsertEntityAsync(user);

        var contract = new UserRegistered(){Id = user.Id, Username = user.Login};
        var contractMain = new UserRegisteredMain() { Id = user.Id, Username = user.Login };
        
        await bus.Publish(contract, cancellationToken);
        await bus.Publish(contractMain, cancellationToken);
        
        return Result.Ok();
    }

    private bool ValidateUser(UserRegisterDto user, out string? error)
    {
        error = null;
        return true;
    }
}