using FluentResults;
using Generic.Mediator;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.RatingService.UseCases.Rating.Commands.UpdateMatchParticipantRating;

public class UpdateMatchParticipantRatingCommand: IRequest<Result>
{
    public MatchEnded MatchResult { get; set; } = null!;
}