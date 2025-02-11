using FluentResults;
using FluentValidation;
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
    IValidator<UserRegisterDto> validator,
    IBus bus): IRequestHandler<RegisterUserCommand, Result>
{
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!Validation.Validation.Validate(request.User, validator, out var errors))
        {
            return Result.Fail(errors);
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
        
        await bus.Publish(contract, cancellationToken);
        
        return Result.Ok();
    }
}