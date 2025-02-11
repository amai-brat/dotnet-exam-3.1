namespace TicTacToe.MainService.Domain.Entities;

public class Game
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public int CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;

    public int MaxRating { get; set; }
    
    public GameStatus Status { get; set; }
}

public enum GameStatus
{
    Started = 0,
    Closed
}