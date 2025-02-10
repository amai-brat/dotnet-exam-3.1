namespace TicTacToe.MainService.Domain.Entities;

public class Game
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public int CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;

    public int MaxRating { get; set; }
    
    public GameStatus Status { get; set; }
}

public enum GameStatus
{
    Created = 0,
    Started,
    Closed
}