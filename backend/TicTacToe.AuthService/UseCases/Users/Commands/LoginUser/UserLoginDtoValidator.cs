using FluentValidation;
using TicTacToe.AuthService.Validation;

namespace TicTacToe.AuthService.UseCases.Users.Commands.LoginUser;

public class UserLoginDtoValidator: AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(r => r.Login)
            .NotNull().WithMessage(ValidationMessages.LoginRequired)
            .NotEmpty().WithMessage(ValidationMessages.LoginRequired);

        RuleFor(r => r.Password)
            .NotNull().WithMessage(ValidationMessages.PasswordRequired)
            .NotEmpty().WithMessage(ValidationMessages.PasswordRequired);
    }
}