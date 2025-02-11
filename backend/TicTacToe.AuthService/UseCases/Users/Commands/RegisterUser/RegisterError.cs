using TicTacToe.AuthService.Abstractions.Error;

namespace TicTacToe.AuthService.UseCases.Users.Commands.RegisterUser;

public class RegisterError(string message) : AppError(ErrorCode, message)
{
    public const string LoginAlreadyExist = "Логин уже занят";
    private const int ErrorCode = 400;
}