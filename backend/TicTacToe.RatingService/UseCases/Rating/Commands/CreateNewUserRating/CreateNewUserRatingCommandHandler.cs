using FluentResults;
using Generic.Mediator;
using TicTacToe.RatingService.Abstractions.Repositories;

namespace TicTacToe.RatingService.UseCases.Rating.Commands.CreateNewUserRating;

public class CreateNewUserRatingCommandHandler(
    IRatingRepository ratingRepository): IRequestHandler<CreateNewUserRatingCommand, Result>
{
    public async Task<Result> Handle(CreateNewUserRatingCommand request, CancellationToken cancellationToken)
    {
        await ratingRepository.InsertAsync(new Entities.Rating()
        {
            UserId = request.User.Id, 
            Login = request.User.Username,
            Score = 0
        });
        
        return Result.Ok();
    }
}