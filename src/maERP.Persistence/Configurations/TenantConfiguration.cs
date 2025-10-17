using maERP.Domain.Entities;
using maERP.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasData(
            new Tenant
            {
                Id = TenantConstants.DefaultTenantId,
                Name = "Default Tenant",
                Description = "Default tenant for initial setup",
                // TenantCode removed
                IsActive = true,
                ContactEmail = "admin@example.com"
            }
        );

        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);

        // TenantCode configuration removed

        builder.Property(q => q.Description)
            .HasMaxLength(500);

        builder.Property(q => q.ContactEmail)
            .HasMaxLength(200);

        // Configure relationship with ApplicationUser through UserTenants
        builder.HasMany(t => t.UserTenants)
            .WithOne(ut => ut.Tenant)
            .HasForeignKey(ut => ut.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}