using TicTacToe.AuthService.Abstractions;
using TicTacToe.AuthService.Abstractions.Repositories;
using TicTacToe.AuthService.Entities;

namespace TicTacToe.AuthService.DataAccess.Repositories;

public class UserRepository(AppDbContext appDbContext): Repository<User>(appDbContext), IUserRepository
{
}