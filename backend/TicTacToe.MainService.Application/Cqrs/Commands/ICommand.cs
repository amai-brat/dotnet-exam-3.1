using FluentResults;
using Generic.Mediator;

namespace TicTacToe.MainService.Application.Cqrs.Commands;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;