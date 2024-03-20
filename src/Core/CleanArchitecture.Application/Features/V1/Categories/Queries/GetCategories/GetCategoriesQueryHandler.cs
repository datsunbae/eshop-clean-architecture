﻿using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Application.Features.V1.Categories.Specs;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, PaginationResponse<CategoryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));   
    }

    public async Task<Result<PaginationResponse<CategoryResponse>>> Handle(
        GetCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var spec = new CategoriesSpec(request);
        var result = await _categoryRepository
            .PaginatedListAsync(
                spec, 
                request.PageNumber, 
                request.PageSize, 
                cancellationToken);

        return result;
    }
}
