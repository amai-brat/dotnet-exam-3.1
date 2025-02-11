namespace TicTacToe.RatingService.UseCases.Rating.Queries.GetGlobalRating;

public class RatingDto
{
    public int Rating { get; set; }
    public string Username { get; set; } = string.Empty;
}