using FluentResults;
using Generic.Mediator;

namespace TicTacToe.AuthService.UseCases.Users.Commands.LoginUser;

public class LoginUserCommand: IRequest<Result<LoginResultDto>>
{
    public UserLoginDto User { get; set; } = null!;
}