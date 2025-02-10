using Generic.Mediator;

namespace TicTacToe.MainService.Application.Cqrs.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>;