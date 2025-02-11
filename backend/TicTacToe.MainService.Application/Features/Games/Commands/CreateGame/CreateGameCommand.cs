using TicTacToe.MainService.Application.Cqrs.Commands;

namespace TicTacToe.MainService.Application.Features.Games.Commands.CreateGame;

public record CreateGameCommand(int MaxRating) : ICommand<CreateGameDto>;