using CleanArchitecture.Application.Common.FileStorage;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Domain.AggregatesModels.Categories;
using CleanArchitecture.Domain.AggregatesModels.Categories.Repository;
using CleanArchitecture.Domain.AggregatesModels.Products;
using CleanArchitecture.Domain.AggregatesModels.Products.Repository;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateProductCommandHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository, 
        IFileStorageService fileStorageService)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _fileStorageService = fileStorageService ?? throw new ArgumentException(nameof(fileStorageService));
    }

    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.GetByIdAsync(request.Id);
        if(product == null)
            return Result.Failure<Guid>(ProductErrors.NotFound);

        Category category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            return Result.Failure<Guid>(CategoryErrors.NotFound);

        string? imagePath = request.Image is not null ?
            await _fileStorageService.UploadAsync<Product>(
                request.Image, 
                FileType.Image, 
                cancellationToken) 
            : null;

        Product productUpdate = product.Update(
            request.Name, 
            request.Description, 
            request.Price,
            imagePath, 
            request.CategoryId);

        await _productRepository.UpdateAsync(productUpdate);

        return product.Id;
    }
}
