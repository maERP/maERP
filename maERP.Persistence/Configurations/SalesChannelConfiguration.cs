using maERP.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class SalesChannelConfiguration : IEntityTypeConfiguration<SalesChannel>
{
    public void Configure(EntityTypeBuilder<SalesChannel> modelBuilder)
    {
        modelBuilder.HasData(
            new SalesChannel
            {
                Id = 1,
                Name = "Kasse Ladengeschäft",
                Type = SalesChannelType.PointOfSale,
                URL = string.Empty,
                Username = string.Empty,
                Password = string.Empty,
                ImportProducts = false,
                ImportCustomers = false,
                ImportOrders = false,
                ExportProducts = false,
                ExportCustomers = false,
                ExportOrders = false,
                WarehouseId = 1
            }
        );

        modelBuilder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
