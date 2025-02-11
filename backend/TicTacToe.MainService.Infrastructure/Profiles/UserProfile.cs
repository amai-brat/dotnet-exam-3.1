using AutoMapper;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;
using TicTacToe.MainService.Application.Features.Users.Commands.CreateUser;
using TicTacToe.MainService.Domain.Entities;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.MainService.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<CreateUserCommand, User>();

        CreateMap<UserRegisteredMain, CreateUserCommand>();
    }
}