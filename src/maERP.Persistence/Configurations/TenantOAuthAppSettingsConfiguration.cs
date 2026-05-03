using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class TenantOAuthAppSettingsConfiguration : IEntityTypeConfiguration<TenantOAuthAppSettings>
{
    public void Configure(EntityTypeBuilder<TenantOAuthAppSettings> builder)
    {
        // One row per (TenantId, Provider). Adding a new provider = new row, no migration.
        builder.HasIndex(s => new { s.TenantId, s.Provider }).IsUnique();

        builder
            .HasOne(s => s.Tenant)
            .WithMany()
            .HasForeignKey(s => s.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        // ClientSecret is encrypted at rest. The converter is wired up in
        // ApplicationDbContext.OnModelCreating so the existing IDataProtector instance is reused.
    }
}
