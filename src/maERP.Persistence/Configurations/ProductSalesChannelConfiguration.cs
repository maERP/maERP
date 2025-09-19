using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class ProductSalesChannelConfiguration : IEntityTypeConfiguration<ProductSalesChannel>
{
    public void Configure(EntityTypeBuilder<ProductSalesChannel> builder)
    {
        builder.Property(e => e.Price)
            .HasPrecision(18, 2);

        builder.Property(e => e.MinimumProfit)
            .HasPrecision(18, 2);
    }
}