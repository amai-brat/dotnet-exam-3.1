using TicTacToe.MainService.Application.Cqrs.Queries;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;

namespace TicTacToe.MainService.Application.Features.Games.Queries.GetGame;

public record GetGameQuery(int Id) : IQuery<GetGameDto>;