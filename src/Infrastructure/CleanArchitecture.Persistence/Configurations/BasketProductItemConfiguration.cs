using CleanArchitecture.Domain.AggregatesModels.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public sealed class BasketProductItemConfiguration : IEntityTypeConfiguration<BasketProductItem>
{
    public void Configure(EntityTypeBuilder<BasketProductItem> builder)
    {
        builder.Property(b => b.ProductId).IsRequired();
        builder.Property(b => b.BasketId).IsRequired();

        builder.HasOne<Basket>()
            .WithMany(b => b.BasketProductItems)
            .HasForeignKey(b => b.BasketId);
    }
}
