using FluentResults;
using Generic.Mediator;

namespace TicTacToe.AuthService.UseCases.Users.Commands.RegisterUser;

public class RegisterUserCommand: IRequest<Result>
{
    public UserRegisterDto User { get; set; } = null!;
}