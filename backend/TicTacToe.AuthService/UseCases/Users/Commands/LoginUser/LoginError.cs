using TicTacToe.AuthService.Abstractions.Error;

namespace TicTacToe.AuthService.UseCases.Users.Commands.LoginUser;

public class LoginError(string message) : AppError(ErrorCode, message)
{
    public const string LoginNotFound = "Пользователя с таким логином не существует";
    public const string PasswordMismatch = "Пароли не совпадают";
    private const int ErrorCode = 403;
}