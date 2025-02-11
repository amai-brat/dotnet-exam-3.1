using TicTacToe.MainService.Application.Cqrs.Queries;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;

namespace TicTacToe.MainService.Application.Features.Users.Queries.GetPersonalInfo;

public record GetPersonalInfoQuery : IQuery<UserDto>;