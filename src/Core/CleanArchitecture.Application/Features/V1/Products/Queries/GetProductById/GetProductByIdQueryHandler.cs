using Ardalis.Specification;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Products.Specs;
using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.AggregatesModels.Products.Repository;
using CleanArchitecture.Domain.Common;
using Mapster;

namespace CleanArchitecture.Application.Features.V1.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        await _productRepository.FirstOrDefaultAsync(
            (ISpecification<Product, ProductResponse>)new ProductByIdWithCategorySpec(request.Id), 
            cancellationToken) ?? 
        Result.Failure<ProductResponse>(ProductErrors.NotFound);
}
