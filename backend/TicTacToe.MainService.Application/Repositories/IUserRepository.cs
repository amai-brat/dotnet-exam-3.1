using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Application.Repositories;

public interface IUserRepository
{
    Task<int> CreateAsync(User user);
    Task<User?> GetByIdAsync(int id);
}