using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Products;

public sealed class Product : BaseEntityRoot
{
    public Product(Guid id) : base(id)
    {
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }
}
