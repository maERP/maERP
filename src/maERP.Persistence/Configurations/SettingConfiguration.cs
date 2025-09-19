using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.HasData(
            new Setting
            {
                Key = "server_hostname",
                Value = "https://localhost:8443"
            }
        );

        builder
            .HasIndex(s => s.Key)
            .IsUnique();
    }
}
