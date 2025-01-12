using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> modelBuilder)
    {
        modelBuilder.HasData(
            new Setting()
            {
                Key = "server_hostname",
                Value = "https://localhost:8443"
            }
        );

        modelBuilder
            .HasIndex(s => s.Key)
            .IsUnique();
    }
}
