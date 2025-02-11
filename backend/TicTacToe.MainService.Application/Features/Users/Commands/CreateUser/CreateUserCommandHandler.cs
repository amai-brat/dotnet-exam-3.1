using AutoMapper;
using FluentResults;
using TicTacToe.MainService.Application.Cqrs.Commands;
using TicTacToe.MainService.Application.Repositories;
using TicTacToe.MainService.Domain.Entities;

namespace TicTacToe.MainService.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler(
    IMapper mapper,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateUserCommand, CreateUserDto>
{
    public async Task<Result<CreateUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        await userRepository.CreateAsync(user);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateUserDto
        {
            Id = user.Id
        };
    }
}