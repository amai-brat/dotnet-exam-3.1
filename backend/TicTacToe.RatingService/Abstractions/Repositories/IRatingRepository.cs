using TicTacToe.RatingService.Entities;

namespace TicTacToe.RatingService.Abstractions.Repositories;

public interface IRatingRepository
{
    Task<Rating?> GetByUserIdAsync(int userId);
    
    Task<List<Rating>> GetAllRatingsAsync();

    Task InsertAsync(Rating rating);
    
    Task UpdateAsync(Rating rating);
    
    Task UpdateManyAsync(List<Rating> ratings);
}