using TicTacToe.RatingService.Abstractions.Error;

namespace TicTacToe.RatingService.UseCases.Rating.Queries.GetPersonalRating;

public class GetPersonalRatingError(string message) : AppError(ErrorCode, message)
{
    public const string LoginIncorrect = "Нет рейтинга для заданного логина";
    private const int ErrorCode = 400;
}