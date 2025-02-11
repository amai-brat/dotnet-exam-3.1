using MassTransit;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.RatingService.Abstractions.Consumers;

public interface IMatchEndedConsumer: IConsumer<MatchEnded>
{
    
}