﻿using System.Reflection;

namespace CleanArchitecture.Api;

public static class AssemblyReference
{
    public static readonly Assembly Program = typeof(Program).Assembly;
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly Assembly Executing = Assembly.GetExecutingAssembly();
}
