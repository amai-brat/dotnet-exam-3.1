using FluentResults;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.MainService.Application.Services;

public interface ITicTacToeGame
{
    Result<GameResult> GetGameResult(int[,] grid);
    Result<RatingPoints> GetRatingPoints(GameResult gameResult, int firstUserid, int secondUserid);
}

public enum GameResult
{
    Continue = 0,
    FirstPlayerWon = 1,
    SecondPlayerWon = 2,
    Draw = 3
}