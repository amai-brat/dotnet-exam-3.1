using FluentResults;
using Generic.Mediator;
using TicTacToe.RatingService.Abstractions.Repositories;

namespace TicTacToe.RatingService.UseCases.Rating.Queries.GetPersonalRating;

public class GetPersonalRatingQueryHandler(
    IRatingRepository ratingRepository): IRequestHandler<GetPersonalRatingQuery, Result<PersonalRatingDto>>
{
    public async Task<Result<PersonalRatingDto>> Handle(GetPersonalRatingQuery request, CancellationToken cancellationToken)
    {
        var rating = await ratingRepository.GetByUserIdAsync(request.UserId);
        
        return rating == null ? 
            Result.Fail(new GetPersonalRatingError(GetPersonalRatingError.LoginIncorrect)) : 
            Result.Ok(new PersonalRatingDto(){Rating = rating.Score});
    }
}