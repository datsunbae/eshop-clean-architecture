using CleanArchitecture.Application.Common.Specification;
using CleanArchitecture.Application.Features.V1.Categories.Models.Responses;
using CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;
using CleanArchitecture.Domain.AggregatesModels.Categories;

namespace CleanArchitecture.Application.Features.V1.Categories.Specs;

public sealed class CategoriesPaginatedSpec : EntitiesByPaginationFilterSpec<Category, CategoryResponse>
{
    public CategoriesPaginatedSpec(GetCategoriesQuery filter) : base(filter) { }
}
