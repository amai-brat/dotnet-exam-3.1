namespace TicTacToe.MainService.Application.Exceptions;

public class NotFoundException(string message) : Exception(message);
