using Generic.Mediator;
using MassTransit;
using TicTacToe.RatingService.Abstractions.Consumers;
using TicTacToe.RatingService.UseCases.Rating.Commands.UpdateMatchParticipantRating;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.RatingService.Consumers;

public class MatchEndedConsumer(IMediator mediator): IMatchEndedConsumer
{
    public async Task Consume(ConsumeContext<MatchEnded> context)
    {
        await mediator.Send(new UpdateMatchParticipantRatingCommand(){MatchResult = context.Message});
    }
}