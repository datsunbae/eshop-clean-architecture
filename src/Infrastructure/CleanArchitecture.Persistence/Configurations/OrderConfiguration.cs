using CleanArchitecture.Domain.AggregatesModels.Orders;
using CleanArchitecture.Domain.AggregatesModels.Shared;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.UserId).IsRequired();

        builder.OwnsOne(
            o => o.UserInformation, userInformationBuider =>
            {
                userInformationBuider.Property(u => u.Phone)
                    .HasMaxLength(20)
                    .IsRequired()
                    .HasConversion(phone => phone.Value, value => new Phone(value));

                //userInformationBuider.Property(u => u.Address)
                //    .IsRequired()
                //    .HasConversion(
                //        address => new { address.Street, address.City }, 
                //        value => new Address(value.Street, value.City));

                userInformationBuider.OwnsOne(
                    u => u.Address, addressBuilder =>
                    {
                        addressBuilder.Property(a => a.Street)
                            .HasMaxLength(100)
                            .IsRequired();

                        addressBuilder.Property(a => a.City)
                            .HasMaxLength(50)
                            .IsRequired();
                    });

                userInformationBuider.Property(u => u.Name)
                    .IsRequired();
            });
    }
}
