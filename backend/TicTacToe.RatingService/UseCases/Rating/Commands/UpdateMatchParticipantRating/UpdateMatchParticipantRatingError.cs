using TicTacToe.RatingService.Abstractions.Error;

namespace TicTacToe.RatingService.UseCases.Rating.Commands.UpdateMatchParticipantRating;

public class UpdateMatchParticipantRatingError(string message) : AppError(ErrorCode, message)
{
    public const string UserIdIncorrect = "Неправильный looserId или winnerId";
    private const int ErrorCode = 400;
}