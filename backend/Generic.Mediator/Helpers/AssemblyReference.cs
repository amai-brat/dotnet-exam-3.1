﻿using System.Reflection;

namespace Generic.Mediator.Helpers;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}