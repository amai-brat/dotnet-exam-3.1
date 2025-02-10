using Generic.Mediator;

namespace TicTacToe.MainService.Application.Cqrs.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> 
    where TQuery : IQuery<TResponse>;