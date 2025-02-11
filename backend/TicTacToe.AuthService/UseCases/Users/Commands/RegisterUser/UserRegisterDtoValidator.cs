using FluentValidation;
using TicTacToe.AuthService.Validation;

namespace TicTacToe.AuthService.UseCases.Users.Commands.RegisterUser;

public class UserRegisterDtoValidator: AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(r => r.Login)
            .NotNull().WithMessage(ValidationMessages.LoginRequired)
            .NotEmpty().WithMessage(ValidationMessages.LoginRequired);
        
        RuleFor(r => r.Password)
            .NotNull().WithMessage(ValidationMessages.PasswordRequired)
            .NotEmpty().WithMessage(ValidationMessages.PasswordRequired)
            .Length(7, int.MaxValue).WithMessage(ValidationMessages.PasswordMustHaveEnoughLength)
            .Matches("(?=.*[a-z])").WithMessage(ValidationMessages.PasswordMustHaveLowerCaseLetter)
            .Matches("(?=.*[A-Z])").WithMessage(ValidationMessages.PasswordMustHaveUpperCaseLetter)
            .Matches("(?=.*\\d)").WithMessage(ValidationMessages.PasswordMustHaveDigit);

        RuleFor(r => r.PasswordConfirm)
            .Equal(r => r.Password).WithMessage(ValidationMessages.PasswordMustMatch);
    }
}