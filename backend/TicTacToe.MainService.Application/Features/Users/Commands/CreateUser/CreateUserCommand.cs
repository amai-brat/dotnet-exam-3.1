using TicTacToe.MainService.Application.Cqrs.Commands;

namespace TicTacToe.MainService.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(int Id, string Username) : ICommand<CreateUserDto>;