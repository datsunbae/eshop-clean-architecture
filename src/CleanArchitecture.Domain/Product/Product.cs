using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Product;

public sealed class Product : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string Description { get; private set; } = string.Empty;
}
