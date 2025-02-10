using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Infrastructure.Data.Repositories;

public class GameRepository : IGameRepository
{
    public Task<List<Game>> GetGamesOrderedByDateAndStatusAsync(int count, int page)
    {
        throw new NotImplementedException();
    }

    public Task<Game> CreateAsync(Game game)
    {
        throw new NotImplementedException();
    }
}