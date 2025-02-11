using Generic.Mediator;

namespace TicTacToe.MainService.Application.Cqrs.Commands;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>;