using TicTacToe.MainService.Application.Cqrs.Queries;

namespace TicTacToe.MainService.Application.Features.Games.Queries.GetGames;

public record GetGamesQuery(int Count, int Page) : IQuery<GetGamesDto>;