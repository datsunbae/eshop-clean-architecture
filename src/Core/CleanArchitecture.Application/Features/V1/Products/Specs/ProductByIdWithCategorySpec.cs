using Ardalis.Specification;
using CleanArchitecture.Domain.Products;

namespace CleanArchitecture.Application.Features.V1.Products.Specs;

public sealed class ProductByIdWithCategorySpec : Specification<Product, ProductResponse>, ISingleResultSpecification
{
    public ProductByIdWithCategorySpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Category);
}
