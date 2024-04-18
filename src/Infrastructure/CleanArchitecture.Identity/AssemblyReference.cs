using System.Reflection;

namespace CleanArchitecture.Identity;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

