using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> modelBuilder)
    {
        modelBuilder.HasData(
            new Warehouse
            {
                Id = 1,
                Name = "Testlager"
            }
        );

        modelBuilder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
