using TicTacToe.MainService.Application.Repositories;

namespace TicTacToe.MainService.Infrastructure.Data.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}