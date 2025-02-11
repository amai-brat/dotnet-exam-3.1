using FluentResults;
using Generic.Mediator;

namespace TicTacToe.MainService.Application.Cqrs.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> 
    where TCommand : IRequest;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> 
    where TCommand : IRequest<Result<TResponse>>;