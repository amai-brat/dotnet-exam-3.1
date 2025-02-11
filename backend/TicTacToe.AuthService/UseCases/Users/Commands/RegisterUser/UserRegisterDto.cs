namespace TicTacToe.AuthService.UseCases.Users.Commands.RegisterUser;

public class UserRegisterDto
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PasswordConfirm { get; set; } = string.Empty;
}