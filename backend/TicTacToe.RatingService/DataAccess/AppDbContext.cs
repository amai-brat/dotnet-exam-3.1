using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TicTacToe.RatingService.Entities;
using TicTacToe.RatingService.Options;

namespace TicTacToe.RatingService.DataAccess;

public class AppDbContext
{
    public readonly IMongoCollection<Rating> Rating;

    public AppDbContext(IOptions<DbOptions> mongoOptions)
    {
        var mongoClient = new MongoClient(
            mongoOptions.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoOptions.Value.DatabaseName);

        Rating = mongoDatabase.GetCollection<Rating>(
            mongoOptions.Value.CollectionName);
    }
}