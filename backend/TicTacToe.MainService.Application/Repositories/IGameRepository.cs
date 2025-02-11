using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Application.Repositories;

public interface IGameRepository
{
    // offset = (page - 1) * count
    Task<List<Game>> GetGamesOrderedByDateAndStatusAsync(int count, int page);
    Task<Game?> GetGameByIdAsync(int id);
    Task<Game> CreateAsync(Game game);
}