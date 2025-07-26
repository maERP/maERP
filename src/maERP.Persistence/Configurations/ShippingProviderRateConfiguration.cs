using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class ShippingProviderRateConfiguration : IEntityTypeConfiguration<ShippingProviderRate>
{
    public void Configure(EntityTypeBuilder<ShippingProviderRate> builder)
    {
        builder.Property(e => e.MaxLength)
            .HasPrecision(18, 4);
            
        builder.Property(e => e.MaxWidth)
            .HasPrecision(18, 4);
            
        builder.Property(e => e.MaxHeight)
            .HasPrecision(18, 4);
            
        builder.Property(e => e.MaxWeight)
            .HasPrecision(18, 4);
    }
}