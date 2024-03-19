using CleanArchitecture.Application.Common.Specification;
using CleanArchitecture.Domain.Categories;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;

public sealed class CategoriesSpec : EntitiesByPaginationFilterSpec<Category, CategoryResponse>
{
    public CategoriesSpec(GetCategoriesQuery filter) : base(filter) { }
}
