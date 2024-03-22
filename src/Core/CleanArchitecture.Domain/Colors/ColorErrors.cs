using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Colors;

public static class ColorErrors
{
    public static Error NotFound = new(
        "Color.NotFound",
        "Color not found!");
}
