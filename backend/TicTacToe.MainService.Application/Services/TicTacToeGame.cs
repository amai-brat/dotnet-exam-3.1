using FluentResults;
using TicTacToe.Shared.Contracts;

namespace TicTacToe.MainService.Application.Services;

public class TicTacToeGame : ITicTacToeGame
{
    public Result<GameResult> GetGameResult(int[,] grid)
    {
        if (grid.GetLength(0) != 3 || grid.GetLength(0) != 3)
        {
            return Result.Fail<GameResult>("Grid size must be 3x3");
        }

        for (int row = 0; row < 3; row++)
        {
            if (grid[row, 0] == grid[row, 1] && grid[row, 0] == grid[row, 2])
            {
                switch (grid[row, 0])
                {
                    case 1: return GameResult.FirstPlayerWon;
                    case 2: return GameResult.SecondPlayerWon;
                }
            }
        }
        
        for (int col = 0; col < 3; col++)
        {
            if (grid[0, col] == grid[1, col] && grid[0, col] == grid[2, col])
            {
                switch (grid[0, col])
                {
                    case 1: return GameResult.FirstPlayerWon;
                    case 2: return GameResult.SecondPlayerWon;
                }
            }
        }

        if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
        {
            switch (grid[0, 0])
            {
                case 1: return GameResult.FirstPlayerWon;
                case 2: return GameResult.SecondPlayerWon;
            }
        }
        
        if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 1])
        {
            switch (grid[0, 2])
            {
                case 1: return GameResult.FirstPlayerWon;
                case 2: return GameResult.SecondPlayerWon;
            }
        }

        var filled = true;
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            if (!filled) break;
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == 0)
                {
                    filled = false;
                }
            }
        }

        return filled 
            ? GameResult.Draw 
            : GameResult.Continue;
    }

    public Result<RatingPoints> GetRatingPoints(GameResult gameResult, int firstUserid, int secondUserid)
    {
        return gameResult switch
        {
            GameResult.FirstPlayerWon => new RatingPoints
            {
                FirstPlayerPoints = new PlayerPoints(firstUserid, 3),
                SecondPlayerPoints = new PlayerPoints(secondUserid, -1)
            },
            GameResult.SecondPlayerWon => new RatingPoints
            {
                FirstPlayerPoints = new PlayerPoints(firstUserid, -1),
                SecondPlayerPoints = new PlayerPoints(secondUserid, 3)
            },
            GameResult.Draw => new RatingPoints
            {
                FirstPlayerPoints = new PlayerPoints(firstUserid, 0),
                SecondPlayerPoints = new PlayerPoints(secondUserid, 0)
            },
            _ => throw new InvalidOperationException("Game is not finished")
        };
    }
}