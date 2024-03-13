using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Common;
using Mapster;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryV2Handler : IQueryHandler<GetCategoriesQueryV2, PaginationResponse<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesQueryV2Handler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<Result<PaginationResponse<CategoryResponse>>> Handle(GetCategoriesQueryV2 request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
            var result = categories.Adapt<PaginationResponse<CategoryResponse>>();
            return result;
        }
    }
}
