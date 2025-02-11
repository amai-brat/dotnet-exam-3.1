using System.Security.Claims;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Http;
using TicTacToe.MainService.Application.Cqrs.Commands;
using TicTacToe.MainService.Application.Exceptions;
using TicTacToe.MainService.Application.Features.Games.Queries.GetGames;
using TicTacToe.MainService.Application.Helpers;
using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommandHandler(
    IUserRepository userRepository,
    IGameRepository gameRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    ) : ICommandHandler<CreateGameCommand, CreateGameDto>
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;
    
    public async Task<Result<CreateGameDto>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContext.User.FindUserId();
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        var game = mapper.Map<Game>(request);
        game.CreatedAt = DateTime.UtcNow;
        game.CreatedBy = user;
        
        game = await gameRepository.CreateAsync(game);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CreateGameDto
        {
            Game = mapper.Map<GameDto>(game)
        };
    }
}