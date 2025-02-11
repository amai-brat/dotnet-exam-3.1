using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TicTacToe.AuthService.DataAccess;

namespace TicTacToe.AuthService.Abstractions.Repositories;

public abstract class Repository<T>(AppDbContext appDbContext): IRepository<T> where T : class
{
    protected readonly AppDbContext AppDbContext = appDbContext;
        
    public virtual async Task<List<T>> GetEntitiesByFilterAsync(Expression<Func<T, bool>> filter) =>
        await AppDbContext.Set<T>()
            .Where(filter)
            .ToListAsync();

    public virtual async Task<T?> GetEntityByFilterAsync(Expression<Func<T, bool>> filter) => 
        await AppDbContext.Set<T>()
            .SingleOrDefaultAsync(filter);

    public virtual async Task InsertEntityAsync(T entity)
    {
        await AppDbContext.AddAsync(entity);
        await AppDbContext.SaveChangesAsync();
    }

    public virtual async Task UpdateEntityAsync(T entity)
    {
        AppDbContext.Update(entity);
        await AppDbContext.SaveChangesAsync();
    }
}
