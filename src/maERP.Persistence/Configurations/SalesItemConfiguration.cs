using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class SalesItemConfiguration : IEntityTypeConfiguration<SalesItem>
{
    public void Configure(EntityTypeBuilder<SalesItem> builder)
    {
        builder.Property(e => e.Price)
            .HasPrecision(18, 2);
    }
}