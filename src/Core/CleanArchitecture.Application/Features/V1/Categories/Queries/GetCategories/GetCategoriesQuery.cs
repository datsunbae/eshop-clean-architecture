using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Features.V1.Categories.Models.Responses;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;

public sealed class GetCategoriesQuery : PaginationFilter, IQuery<PaginationResponse<CategoryResponse>>;
