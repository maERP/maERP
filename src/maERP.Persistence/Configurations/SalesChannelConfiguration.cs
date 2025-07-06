using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class SalesChannelConfiguration : IEntityTypeConfiguration<SalesChannel>
{
    public void Configure(EntityTypeBuilder<SalesChannel> modelBuilder)
    {
        modelBuilder.HasMany(sc => sc.Warehouses)
            .WithMany(w => w.SalesChannels)
            .UsingEntity(j => j.ToTable("SalesChannelWarehouses"));

        modelBuilder.HasData(
            new SalesChannel
            {
                Id = 1,
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

        modelBuilder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
