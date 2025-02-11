using FluentResults;
using Generic.Mediator;

namespace TicTacToe.MainService.Application.Cqrs.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;