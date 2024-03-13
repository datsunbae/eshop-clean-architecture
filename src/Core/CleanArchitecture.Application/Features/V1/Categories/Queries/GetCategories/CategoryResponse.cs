using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;

public class CategoryResponse : AuditResponse
{
    public string Name { get; init; }
}
