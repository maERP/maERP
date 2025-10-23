using maERP.Domain.Entities;
using maERP.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class UserTenantConfiguration : IEntityTypeConfiguration<UserTenant>
{
    public void Configure(EntityTypeBuilder<UserTenant> builder)
    {
        builder.HasKey(ut => new { ut.UserId, ut.TenantId });

        builder.HasOne(ut => ut.User)
            .WithMany(u => u.UserTenants)
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ut => ut.Tenant)
            .WithMany(t => t.UserTenants)
            .HasForeignKey(ut => ut.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ut => ut.IsDefault).HasDefaultValue(false);
        builder.Property(ut => ut.RoleManageUser).HasDefaultValue(false);
        builder.Property(ut => ut.RoleManageTenant).HasDefaultValue(false);

        // Seed data: Link default users to default tenant
        builder.HasData(
            new UserTenant
            {
                Id = Guid.NewGuid(),
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9", // admin@localhost.com
                TenantId = TenantConstants.DefaultTenantId,
                IsDefault = true,
                RoleManageUser = true, // Admin can manage users
                RoleManageTenant = true, // Admin can manage tenant
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            }
        );
    }
}
