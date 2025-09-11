using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class SalesChannelConfiguration : IEntityTypeConfiguration<SalesChannel>
{
    public void Configure(EntityTypeBuilder<SalesChannel> builder)
    {
        builder.HasMany(sc => sc.Warehouses)
            .WithMany(w => w.SalesChannels)
            .UsingEntity(j => j.ToTable("SalesChannelWarehouses"));

        builder.HasData(
            new SalesChannel
            {
                Id = new Guid("88888888-8888-8888-8888-888888888888"),
                TenantId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Demo tenant ID
                Name = "Kasse Ladengeschäft",
                Type = SalesChannelType.PointOfSale,
                Url = string.Empty,
                Username = string.Empty,
                Password = string.Empty,
                ImportProducts = false,
                ImportCustomers = false,
                ImportOrders = false,
                ExportProducts = false,
                ExportCustomers = false,
                ExportOrders = false
            }
        );

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
