using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Categories;
using CleanArchitecture.Domain.Common;
using Mapster;

namespace CleanArchitecture.Application.Features.V1.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<Result<Guid>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            return Result.Failure<Guid>(CategoryErrors.NotFound);

        var updateCategory = new Category(category.Id, request.Name);

        await _categoryRepository.UpdateAsync(updateCategory, cancellationToken);

        return category.Id;
    }
}
