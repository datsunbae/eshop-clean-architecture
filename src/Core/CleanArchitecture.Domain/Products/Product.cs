using CleanArchitecture.Domain.Categories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Products.Events;

namespace CleanArchitecture.Domain.Products;

public class Product : BaseEntityRoot
{
    public Product(
        Guid id,
        string name,
        string description,
        decimal price,
        Guid categoryId) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public Product Update(
        string? name,
        string? description,
        decimal? price,
        Guid? categoryId)
    {
        if(name is not null && Name?.Equals(name) is not true) 
            Name = name;

        if(description is not null &&  Description?.Equals(description) is not true) 
            Description = description;

        if(price.HasValue && Price != price)
            Price = price.Value;

        if(categoryId.HasValue && CategoryId.Equals(categoryId.Value) is not true && categoryId.Value != Guid.Empty)
            CategoryId = categoryId.Value;

        RaiseDomainEvent(new ProductUpdatedDomainEvent(Id));

        return this;
    }
}
