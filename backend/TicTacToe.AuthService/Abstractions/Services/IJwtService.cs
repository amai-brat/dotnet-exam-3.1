using TicTacToe.AuthService.Entities;

namespace TicTacToe.AuthService.Abstractions.Services;

public interface IJwtService
{
    string CreateJwtToken(User user);
}