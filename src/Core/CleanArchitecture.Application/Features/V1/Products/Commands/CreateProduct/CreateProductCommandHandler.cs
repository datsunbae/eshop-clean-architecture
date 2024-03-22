using CleanArchitecture.Application.Common.FileStorage;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Categories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Products;
using Mapster;

namespace CleanArchitecture.Application.Features.V1.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFileStorageService _fileStorageService;
    public CreateProductCommandHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository, 
        IFileStorageService fileStorageService)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _fileStorageService = fileStorageService ?? throw new ArgumentNullException(nameof(fileStorageService));
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            return Result.Failure<Guid>(CategoryErrors.NotFound);

        string? imagePath = request.Image is not null ?
            await _fileStorageService.UploadAsync<Product>(
                request.Image,
                FileType.Image, 
                cancellationToken)
            : null;

        var product = new Product(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.Price,
            imagePath,
            request.CategoryId);

        var result = await _productRepository.AddAsync(product);

        return result.Id;
    }
}
