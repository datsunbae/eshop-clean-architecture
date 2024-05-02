using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Features.V1.Categories.Models.Responses;

namespace CleanArchitecture.Application.Features.V1.Products.Models.Responses;

public sealed class ProductResponse : IAuditResponse
{
    public Guid Id { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public Guid LastModifiedBy { get; init; }
    public DateTime? LastModifiedOn { get; init; }
    public DateTime? DeletedOn { get; init; }
    public Guid? DeletedBy { get; init; }

    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string? ImagePath { get; init; }
    public Guid CategoryId { get; init; }
    public CategoryResponse Category { get; init; }
}
