using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Categories;
using CleanArchitecture.Domain.Common;
using Mapster;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        if (category == null)
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);

        return Result.Success(category.Adapt<CategoryResponse>());
    }
}
