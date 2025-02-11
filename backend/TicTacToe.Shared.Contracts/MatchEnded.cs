namespace TicTacToe.Shared.Contracts;

public class MatchEnded
{
    public int WinnerId { get; set; }
    public int LooserId { get; set; }
}