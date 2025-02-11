using FluentResults;
using Generic.Mediator;
using TicTacToe.RatingService.Abstractions.Repositories;

namespace TicTacToe.RatingService.UseCases.Rating.Commands.UpdateMatchParticipantRating;

public class UpdateMatchParticipantRatingCommandHandler(
    IRatingRepository ratingRepository): IRequestHandler<UpdateMatchParticipantRatingCommand, Result>
{
    public async Task<Result> Handle(UpdateMatchParticipantRatingCommand request, CancellationToken cancellationToken)
    {
        var winner = await ratingRepository.GetByUserIdAsync(request.MatchResult.WinnerId);
        var looser = await ratingRepository.GetByUserIdAsync(request.MatchResult.LooserId);

        if (winner is null || looser is null)
        {
            return Result.Fail(
                new UpdateMatchParticipantRatingError(UpdateMatchParticipantRatingError.UserIdIncorrect));
        }

        winner.Score += 3;
        looser.Score -= 1;

        await ratingRepository.UpdateManyAsync([winner, looser]);
        
        return Result.Ok();
    }
}