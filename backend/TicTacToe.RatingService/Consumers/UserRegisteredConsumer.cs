using Generic.Mediator;
using MassTransit;
using TicTacToe.RatingService.Abstractions.Consumers;
using TicTacToe.RatingService.UseCases.Rating.Commands.CreateNewUserRating;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.RatingService.Consumers;

public class UserRegisteredConsumer(IMediator mediator): IUserRegisteredConsumer
{
    public async Task Consume(ConsumeContext<UserRegistered> context)
    {
        await mediator.Send(new CreateNewUserRatingCommand(){User = context.Message});
    }
}