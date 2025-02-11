using FluentResults;
using Generic.Mediator;
using TicTacToe.RatingService.Abstractions.Repositories;

namespace TicTacToe.RatingService.UseCases.Rating.Queries.GetGlobalRating;

public class GetGlobalRatingQueryHandler(
    IRatingRepository ratingRepository): IRequestHandler<GetGlobalRatingQuery, Result<GlobalRatingDto>>
{
    public async Task<Result<GlobalRatingDto>> Handle(GetGlobalRatingQuery request, CancellationToken cancellationToken)
    {
        var ratings = await ratingRepository.GetAllRatingsAsync();
        
        return Result.Ok(new GlobalRatingDto()
        {
            Ratings = ratings
                .Select(r => new RatingDto(){Rating = r.Score, Username = r.Login})
                .OrderByDescending(r => r.Rating)
                .ToList()
        });
    }
}