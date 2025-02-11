using FluentResults;
using Generic.Mediator;

namespace TicTacToe.MainService.Application.Cqrs.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> 
    where TQuery : IRequest<Result<TResponse>>;