using CleanArchitecture.Domain.AggregatesModels.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(o => o.OrderId).IsRequired();
        builder.Property(o => o.ProductId).IsRequired();
        builder.Property(o => o.Price).IsRequired();
        builder.Property(o => o.Quantity).IsRequired();

        builder.HasOne<Order>()
            .WithMany(o => o.OrderItems)
            .HasForeignKey(o => o.OrderId);
    }
}
