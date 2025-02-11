using AutoMapper;
using TicTacToe.MainService.Application.Features.Games.Commands.CreateGame;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;
using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Infrastructure.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDto>();

        CreateMap<CreateGameCommand, Game>();
    }
}