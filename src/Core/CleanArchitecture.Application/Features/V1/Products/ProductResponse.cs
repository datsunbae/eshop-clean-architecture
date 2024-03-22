using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Categories;

namespace CleanArchitecture.Application.Features.V1.Products;

public sealed class ProductResponse: AuditResponse
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string? ImagePath { get; init; }
    public Guid CategoryId { get; init; }
    public CategoryResponse Category { get; init; }
}
