namespace TicTacToe.Shared.Contracts;

public record PlayerPoints(int UserId, int Points);

public class RatingPoints
{
    public required PlayerPoints FirstPlayerPoints { get; set; }
    public required PlayerPoints SecondPlayerPoints { get; set; }
}