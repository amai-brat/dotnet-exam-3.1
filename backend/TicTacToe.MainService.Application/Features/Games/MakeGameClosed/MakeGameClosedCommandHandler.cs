using FluentResults;
using TicTacToe.MainService.Application.Cqrs.Commands;
using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Application.Features.Games.MakeGameClosed;

public class MakeGameClosedCommandHandler(
    IGameRepository gameRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<MakeGameClosedCommand>
{
    public async Task<Result> Handle(MakeGameClosedCommand request, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetGameByIdAsync(request.Id);
        if (game == null)
        {
            return Result.Fail("Game not found");
        }
        
        game.Status = GameStatus.Closed;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }
}