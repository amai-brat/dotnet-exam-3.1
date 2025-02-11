using TicTacToe.MainService.Application.Cqrs.Commands;

namespace TicTacToe.MainService.Application.Features.Games.MakeGameClosed;

public record MakeGameClosedCommand(int Id) : ICommand;