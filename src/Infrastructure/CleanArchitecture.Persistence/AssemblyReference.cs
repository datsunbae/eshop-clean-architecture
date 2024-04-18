using System.Reflection;

namespace CleanArchitecture.Persistence;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
