using Ardalis.Specification;
using CleanArchitecture.Application.Features.V1.Products.Models.Responses;
using CleanArchitecture.Domain.AggregatesModels.Products;

namespace CleanArchitecture.Application.Features.V1.Products.Specs;

public sealed class ProductByIdWithCategorySpec : Specification<Product, ProductResponse>
{
    public ProductByIdWithCategorySpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Category);
}
