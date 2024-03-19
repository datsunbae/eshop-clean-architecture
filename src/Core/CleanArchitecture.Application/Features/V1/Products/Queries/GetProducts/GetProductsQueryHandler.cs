using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Application.Features.V1.Products.Specs;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PaginationResponse<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<PaginationResponse<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var spec = new ProductsWithCategorySpec(request);

        return
            await _productRepository.PaginatedListAsync(
                spec,
                request.PageSize,
                request.PageNumber,
                cancellationToken);
    }
}
