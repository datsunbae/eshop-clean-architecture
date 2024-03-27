using Ardalis.Specification;
using CleanArchitecture.Application.Common.Specification;
using CleanArchitecture.Application.Features.V1.Products.Models.Responses;
using CleanArchitecture.Application.Features.V1.Products.Queries.GetProducts;
using CleanArchitecture.Domain.AggregatesModels.Products;

namespace CleanArchitecture.Application.Features.V1.Products.Specs;

public sealed class ProductsWithCategorySpec : EntitiesByPaginationFilterSpec<Product, ProductResponse>
{
    public ProductsWithCategorySpec(GetProductsQuery request) : base(request)
    {
        Query
            .Include(p => p.Category);
    }
}
