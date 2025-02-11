namespace TicTacToe.AuthService.Abstractions.Services;

public interface IHasherService
{
    string GetHash(string value);
    bool Equals(string hashValue, string value);
}