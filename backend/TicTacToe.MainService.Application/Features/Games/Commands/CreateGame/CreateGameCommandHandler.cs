using AutoMapper;
using TicTacToe.MainService.Application.Cqrs.Commands;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;
using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommandHandler(
    IGameRepository gameRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateGameCommand, CreateGameDto>
{
    public async Task<CreateGameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var game = mapper.Map<Game>(request);
        game = await gameRepository.CreateAsync(game);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CreateGameDto
        {
            Game = mapper.Map<GameDto>(game)
        };
    }
}