namespace TicTacToe.RatingService.Entities;

public class Rating
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public int Score { get; set; }
}