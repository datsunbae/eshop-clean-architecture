using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Categories;

public sealed class Category : BaseEntityRoot
{
    public Category(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; } = string.Empty;
}
