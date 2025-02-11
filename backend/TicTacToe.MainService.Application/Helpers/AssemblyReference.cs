using System.Reflection;

namespace TicTacToe.MainService.Application.Helpers;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}