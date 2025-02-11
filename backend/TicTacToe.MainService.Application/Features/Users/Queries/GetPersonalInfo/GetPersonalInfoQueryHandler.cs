using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Http;
using TicTacToe.MainService.Application.Cqrs.Queries;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;
using TicTacToe.MainService.Application.Helpers;
using TicTacToe.MainService.Application.Repositories;

namespace TicTacToe.MainService.Application.Features.Users.Queries.GetPersonalInfo;

public class GetPersonalInfoQueryHandler(
    IUserRepository userRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor
    ) : IQueryHandler<GetPersonalInfoQuery, UserDto>
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;
    public async Task<Result<UserDto>> Handle(GetPersonalInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(_httpContext.User.FindUserId());
        if (user == null)
        {
            return Result.Fail<UserDto>("User not found");
        }
        
        return mapper.Map<UserDto>(user);
    }
}