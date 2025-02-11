using FluentResults;
using Generic.Mediator;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.RatingService.UseCases.Rating.Commands.CreateNewUserRating;

public class CreateNewUserRatingCommand: IRequest<Result>
{
    public UserRegistered User { get; set; } = null!;
}