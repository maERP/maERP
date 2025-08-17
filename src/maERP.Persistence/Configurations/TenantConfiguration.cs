using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> modelBuilder)
    {
        modelBuilder.HasData(
            new Tenant
            {
                Id = 1,
                Name = "Default Tenant",
                Description = "Default tenant for initial setup",
                TenantCode = "DEFAULT",
                IsActive = true,
                ContactEmail = "admin@example.com"
            }
        );

        modelBuilder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Property(q => q.TenantCode)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.HasIndex(q => q.TenantCode)
            .IsUnique();

        modelBuilder.Property(q => q.Description)
            .HasMaxLength(500);

        modelBuilder.Property(q => q.ContactEmail)
            .HasMaxLength(200);

        // Configure relationship with ApplicationUser through UserTenants
        modelBuilder.HasMany(t => t.UserTenants)
            .WithOne(ut => ut.Tenant)
            .HasForeignKey(ut => ut.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}