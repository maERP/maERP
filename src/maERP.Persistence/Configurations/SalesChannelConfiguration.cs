using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace maERP.Persistence.Configurations;

public class SalesChannelConfiguration : IEntityTypeConfiguration<SalesChannel>
{
    public void Configure(EntityTypeBuilder<SalesChannel> builder)
    {
        builder.HasMany(sc => sc.Warehouses)
            .WithMany(w => w.SalesChannels)
            .UsingEntity<Dictionary<string, object>>(
                "SalesChannelWarehouses",
                j => j
                    .HasOne<Warehouse>()
                    .WithMany()
                    .HasForeignKey("WarehousesId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<SalesChannel>()
                    .WithMany()
                    .HasForeignKey("SalesChannelsId")
                    .OnDelete(DeleteBehavior.Cascade));

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
                ImportSaless = false,
                ExportProducts = false,
                ExportCustomers = false,
                ExportSaless = false
            }
        );

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(q => q.MarketplaceId)
            .HasMaxLength(64);

        // Encrypted at rest — stored ciphertext is much longer than the plain credential.
        builder.Property(q => q.Password).HasMaxLength(4096);
        builder.Property(q => q.AccessToken).HasMaxLength(8192);
        builder.Property(q => q.RefreshToken).HasMaxLength(8192);
    }
}
