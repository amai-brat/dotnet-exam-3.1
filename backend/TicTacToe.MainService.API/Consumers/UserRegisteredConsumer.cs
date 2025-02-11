using AutoMapper;
using Generic.Mediator;
using MassTransit;
using TicTacToe.MainService.Application.Features.Users.Commands.CreateUser;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.MainService.Consumers;

public class UserRegisteredConsumer(
    IMediator mediator,
    IMapper mapper) : IConsumer<UserRegisteredMain>
{
    public async Task Consume(ConsumeContext<UserRegisteredMain> context)
    {
        var command = mapper.Map<CreateUserCommand>(context.Message);
        await mediator.Send(command);
    }
}