﻿using CleanArchitecture.Domain.AggregatesModels.Categories;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.AggregatesModels.Products;

public sealed class Product : BaseEntityRoot
{
    private Product(
        string name,
        string? description,
        decimal price,
        string? imagePath,
        Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        ImagePath = imagePath;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public string? ImagePath { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; }

    public static Product Create(
        string name,
        string descirtion,
        decimal price,
        string imagePath,
        Guid categoryId)
    {
        Product product = new Product(
            name,
            descirtion,
            price,
            imagePath,
            categoryId);

        return product;
    }

    public Product Update(
        string? name,
        string? description,
        decimal? price,
        string? imagePath,
        Guid? categoryId)
    {
        if (name is not null && Name?.Equals(name) is not true)
            Name = name;

        if (description is not null && Description?.Equals(description) is not true)
            Description = description;

        if (price.HasValue && Price != price)
            Price = price.Value;

        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true)
            ImagePath = imagePath;

        if (categoryId.HasValue && CategoryId.Equals(categoryId.Value) is not true && categoryId.Value != Guid.Empty)
            CategoryId = categoryId.Value;

        return this;
    }

}
