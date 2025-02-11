using System.Security.Cryptography;
using System.Text;
using TicTacToe.AuthService.Abstractions.Services;

namespace TicTacToe.AuthService.Services;

public class HasherService: IHasherService
{
    public bool Equals(string hashValue, string value) =>
        hashValue == GetHash(value);

    public string GetHash(string value)
    {
        var inputBytes = Encoding.UTF8.GetBytes(value);
        var inputHash = SHA512.HashData(inputBytes);
        return Convert.ToHexString(inputHash);
    }
}