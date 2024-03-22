using CleanArchitecture.Domain.Categories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Products.Enums;
using CleanArchitecture.Domain.Products.Events;

namespace CleanArchitecture.Domain.Products;

public class Product : BaseEntityRoot
{
    public Product(
        Guid id,
        string name,
        string? description,
        decimal price,
        string? imagePath,
        OperatingSystemEnum operatingSystem,
        Guid categoryId) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        ImagePath = imagePath;
        OperatingSystem = operatingSystem;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public string? ImagePath { get; private set; }

    public OperatingSystemEnum OperatingSystem { get; private set; }

    public Guid CategoryId { get; private set; }

    public virtual Category Category { get; private set; }

    public Product Update(
        string? name,
        string? description,
        decimal? price,
        string? imagePath,
        OperatingSystemEnum? operatingSystem,
        Guid? categoryId)
    {
        if(name is not null && Name?.Equals(name) is not true) 
            Name = name;

        if(description is not null &&  Description?.Equals(description) is not true) 
            Description = description;

        if(price.HasValue && Price != price)
            Price = price.Value;

        if(imagePath is not null && ImagePath?.Equals(imagePath) is not true)
            ImagePath = imagePath;

        if(categoryId.HasValue && CategoryId.Equals(categoryId.Value) is not true && categoryId.Value != Guid.Empty)
            CategoryId = categoryId.Value;

        if (operatingSystem is not null && OperatingSystem != operatingSystem)
            OperatingSystem = operatingSystem.Value;

        RaiseDomainEvent(new ProductUpdatedDomainEvent(Id));

        return this;
    }
}
