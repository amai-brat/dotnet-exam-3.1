namespace TicTacToe.AuthService.Options;

public class RabbitMqOptions
{
    public string Hostname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}