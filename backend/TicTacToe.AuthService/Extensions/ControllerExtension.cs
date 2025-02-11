using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.AuthService.Extensions;

public static class ControllerExtension
{
    public static IActionResult ErrorResult(this ControllerBase controller, IError error)
    {
        if (error is Error errorObj)
            return controller.StatusCode((int)errorObj.Metadata[Abstractions.Error.AppError.ErrorCode], error.Message);

        throw new ArgumentException("Получен тип данных не содержащий ErrorCode");
    }
}