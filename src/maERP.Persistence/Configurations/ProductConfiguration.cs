using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Price)
            .HasPrecision(18, 2);

        builder.Property(e => e.Msrp)
            .HasPrecision(18, 2);

        builder.Property(e => e.Weight)
            .HasPrecision(18, 4);

        builder.Property(e => e.Width)
            .HasPrecision(18, 4);

        builder.Property(e => e.Height)
            .HasPrecision(18, 4);

        builder.Property(e => e.Depth)
            .HasPrecision(18, 4);
    }
}