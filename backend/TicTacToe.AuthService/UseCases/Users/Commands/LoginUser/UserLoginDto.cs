namespace TicTacToe.AuthService.UseCases.Users.Commands.LoginUser;

public class UserLoginDto
{ 
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}