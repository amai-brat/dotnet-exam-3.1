using FluentResults;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.MainService.Application.Services;

public interface ITicTacToeGame
{
    Result<GameResult> GetGameResult(int[,] grid);
    Result<MatchEnded> GetMatchResult(GameResult gameResult, int firstUserId, int secondUserId);
}

public enum GameResult
{
    Continue = 0,
    FirstPlayerWon = 1,
    SecondPlayerWon = 2,
    Draw = 3
}