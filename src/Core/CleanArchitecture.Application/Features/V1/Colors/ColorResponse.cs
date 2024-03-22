using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Features.V1.Colors;

public sealed class ColorResponse : AuditResponse
{
    public string Name { get; init; }
}
