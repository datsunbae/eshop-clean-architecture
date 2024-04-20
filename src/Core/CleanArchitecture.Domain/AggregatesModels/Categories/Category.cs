using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Categories;

public sealed class Category : BaseEntityRoot
{
    private Category(string name)
    {
        Name = name;
    }

    public string Name { get; private set; } = string.Empty;

    public static Category Create(string name)
    {
        Category category = new Category(name);
        return category;
    }

    public Category Update(string name)
    {
        if (name is not null && Name.Equals(name) is not true)
            Name = name;

        return this;
    }
}
