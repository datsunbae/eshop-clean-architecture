﻿using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.V1.Categories.Models.Responses;
using CleanArchitecture.Domain.AggregatesModels.Categories;
using CleanArchitecture.Domain.Common;
using Mapster;

namespace CleanArchitecture.Application.Features.V1.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<Result<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name);

        return (await _categoryRepository.AddAsync(category))
            .Adapt<CategoryResponse>();
    }
}
