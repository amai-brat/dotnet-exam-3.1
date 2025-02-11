namespace TicTacToe.MainService.Application.Exceptions;

public class ValidationException(string message) : Exception(message);
