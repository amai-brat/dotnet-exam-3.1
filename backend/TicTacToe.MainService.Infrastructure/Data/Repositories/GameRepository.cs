using Microsoft.EntityFrameworkCore;
using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Infrastructure.Data.Repositories;

public class GameRepository(AppDbContext dbContext) : IGameRepository
{
    public async Task<List<Game>> GetGamesOrderedByDateAndStatusAsync(int count, int page)
    {
        var games = await dbContext.Games
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Status)
            .Include(x => x.CreatedBy)
            .Skip(count * (page - 1))
            .Take(count)
            .ToListAsync();

        return games;
    }

    public async Task<Game?> GetGameByIdAsync(int id)
    {
        var game = await dbContext.Games.FindAsync(id);
        return game;
    }

    public async Task<Game> CreateAsync(Game game)
    {
        var entry = await dbContext.Games.AddAsync(game);
        return entry.Entity;
    }

    public async Task DeleteClosedGamesAsync()
    {
        var closedGames = await dbContext.Games.Where(x => x.Status == GameStatus.Closed).ToListAsync();
        dbContext.Games.RemoveRange(closedGames);
    }
}