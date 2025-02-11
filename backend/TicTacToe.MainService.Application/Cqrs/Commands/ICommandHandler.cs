using FluentResults;
using Generic.Mediator;

namespace TicTacToe.MainService.Application.Cqrs.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result> 
    where TCommand : IRequest<Result>;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> 
    where TCommand : IRequest<Result<TResponse>>;