using Ardalis.Specification;
using CleanArchitecture.Application.Common.Specification;
using CleanArchitecture.Domain.Categories;

namespace CleanArchitecture.Application.Features.V1.Categories.Queries.GetCategories;

public class CategoriesSpec : EntitiesByPaginationFilterSpec<Category, CategoryResponse>
{
    public CategoriesSpec(GetCategoriesQuery filter) : base(filter) =>
        Query
            .Where(c => c.DeletedOn == null);
}
