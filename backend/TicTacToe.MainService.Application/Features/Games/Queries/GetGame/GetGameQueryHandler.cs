using AutoMapper;
using FluentResults;
using TicTacToe.MainService.Application.Cqrs.Queries;
using TicTacToe.MainService.Application.Exceptions;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;
using TicTacToe.MainService.Application.Repositories;

namespace TicTacToe.MainService.Application.Features.Games.Queries.GetGame;

public class GetGameQueryHandler(
    IMapper mapper,
    IGameRepository gameRepository) : IQueryHandler<GetGameQuery, GetGameDto>
{
    public async Task<Result<GetGameDto>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetGameByIdAsync(request.Id);
        if (game == null)
        {
            return Result.Fail<GetGameDto>($"Game {request.Id} not found");
        }

        return new GetGameDto
        {
            Game = mapper.Map<GameDto>(game)
        };
    }
}