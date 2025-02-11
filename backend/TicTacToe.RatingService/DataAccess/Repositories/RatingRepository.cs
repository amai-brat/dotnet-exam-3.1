using MongoDB.Driver;
using TicTacToe.RatingService.Abstractions.Repositories;
using TicTacToe.RatingService.Entities;

namespace TicTacToe.RatingService.DataAccess.Repositories;

public class RatingRepository(AppDbContext appDbContext): IRatingRepository
{

    public async Task<Rating?> GetByUserIdAsync(int userId) =>
        await appDbContext.Rating.Find(x => x.UserId == userId).FirstOrDefaultAsync();
    
    public async Task<List<Rating>> GetAllRatingsAsync() => 
        await appDbContext.Rating.Find(x => true).ToListAsync();
    
    public async Task InsertAsync(Rating newData) =>
        await appDbContext.Rating.InsertOneAsync(newData);

    public async Task UpdateAsync(Rating rating) =>
        await appDbContext.Rating.ReplaceOneAsync(x => x.Id == rating.Id, rating);

    public async Task UpdateManyAsync(List<Rating> ratings)
    {
        foreach (var rating in ratings)
        {
            await UpdateAsync(rating);
        }
    }
}