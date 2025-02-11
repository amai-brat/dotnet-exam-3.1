using System.Linq.Expressions;

namespace TicTacToe.AuthService.Abstractions.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity?> GetEntityByFilterAsync(Expression<Func<TEntity, bool>> filter);
    Task<List<TEntity>> GetEntitiesByFilterAsync(Expression<Func<TEntity, bool>> filter);
    Task InsertEntityAsync(TEntity entity);
    Task UpdateEntityAsync(TEntity entity);
}