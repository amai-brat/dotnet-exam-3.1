namespace TicTacToe.MainService.Application.Exceptions;

public class UnauthorizedException(string message) : Exception(message);