using FluentResults;
using Generic.Mediator;

namespace TicTacToe.RatingService.UseCases.Rating.Queries.GetPersonalRating;

public class GetPersonalRatingQuery: IRequest<Result<PersonalRatingDto>>
{
    public int UserId { get; set; }
}