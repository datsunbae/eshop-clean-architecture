using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;

public sealed class GetCategoriesQueryV2 : PaginationFilter ,IQuery<PaginationResponse<CategoryResponse>>
{
}
