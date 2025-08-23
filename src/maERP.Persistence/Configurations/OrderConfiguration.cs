using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.Subtotal)
            .HasPrecision(18, 2);

        builder.Property(e => e.ShippingCost)
            .HasPrecision(18, 2);

        builder.Property(e => e.TotalTax)
            .HasPrecision(18, 2);

        builder.Property(e => e.Total)
            .HasPrecision(18, 2);
    }
}