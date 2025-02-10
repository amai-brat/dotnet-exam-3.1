using AutoMapper;
using TicTacToe.MainService.Application.Cqrs.Queries;
using TicTacToe.MainService.Application.Repositories;

namespace TicTacToe.MainService.Application.Features.Games.Queries.GetGames;

public class GetGamesQueryHandler(
    IGameRepository gameRepository,
    IMapper mapper
    ): IQueryHandler<GetGamesQuery, GetGamesDto>
{
    public async Task<GetGamesDto> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await gameRepository.GetGamesOrderedByDateAndStatusAsync(request.Count, request.Page);
        var dtos = mapper.Map<List<GameDto>>(games);
        
        return new GetGamesDto
        {
            Games = dtos
        };
    }
}