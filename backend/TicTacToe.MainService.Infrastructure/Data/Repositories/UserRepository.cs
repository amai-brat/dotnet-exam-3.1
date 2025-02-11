using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<int> CreateAsync(User user)
    {
        var entry = await dbContext.Users.AddAsync(user);
        return entry.Entity.Id;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await dbContext.Users.FindAsync(id);
        return user;
    }
}