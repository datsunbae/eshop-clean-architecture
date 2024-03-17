using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Features.V1.Categories;

public sealed class CategoryResponse : AuditResponse
{
    public string Name { get; init; }
}
