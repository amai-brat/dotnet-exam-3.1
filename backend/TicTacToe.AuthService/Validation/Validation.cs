using FluentValidation;

namespace TicTacToe.AuthService.Validation;

public static class Validation
{
    public static bool Validate<T>(T obj, IValidator<T> validator, out string? error)
    {
        var validationResult = validator.Validate(obj);

        error = !validationResult.IsValid ? 
            validationResult.Errors.First().ErrorMessage : 
            null;
        
        return validationResult.IsValid;
    }
}