using FluentResults;
using FluentValidation;
using Generic.Mediator;
using TicTacToe.AuthService.Abstractions.Repositories;
using TicTacToe.AuthService.Abstractions.Services;

namespace TicTacToe.AuthService.UseCases.Users.Commands.LoginUser;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IHasherService hasherService,
    IJwtService jwtService,
    IValidator<UserLoginDto> validator): IRequestHandler<LoginUserCommand, Result<LoginResultDto>>
{
    public async Task<Result<LoginResultDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (!Validation.Validation.Validate(request.User, validator, out var errors))
        {
            return Result.Fail(errors);
        }

        var dbUser = await userRepository.GetEntityByFilterAsync(u => u.Login == request.User.Login);
        if (dbUser is null)
        {
            return Result.Fail(new LoginError(LoginError.LoginNotFound));
        } 
        if (!hasherService.Equals(dbUser.Password, request.User.Password))
        {
            return Result.Fail(new LoginError(LoginError.PasswordMismatch));
        }
        
        var jwtToken = jwtService.CreateJwtToken(dbUser);
        
        return Result.Ok(new LoginResultDto(){JwtToken = jwtToken});
    }
}