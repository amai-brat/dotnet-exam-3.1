namespace TicTacToe.AuthService.Abstractions.Error;

public abstract class AppError: FluentResults.Error
{
    public const string ErrorCode = "ErrorCode";

    protected AppError(int errorCode, string message): base(message)
    {
        WithMetadata(ErrorCode, errorCode);
    }
}