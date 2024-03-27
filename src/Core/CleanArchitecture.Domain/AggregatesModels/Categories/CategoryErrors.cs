using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Categories;

public static class CategoryErrors
{
    public static readonly Error NotFound = new(
        "Category.NotFound",
        "Category not found!");
}
