﻿using CleanArchitecture.Domain.AggregatesModels.Categories;
using CleanArchitecture.Domain.AggregatesModels.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne<Category>(c => c.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId);
    }
}
