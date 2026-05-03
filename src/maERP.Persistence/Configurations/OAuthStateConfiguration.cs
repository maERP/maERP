using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class OAuthStateConfiguration : IEntityTypeConfiguration<OAuthState>
{
    public void Configure(EntityTypeBuilder<OAuthState> builder)
    {
        builder.HasIndex(s => s.StateToken).IsUnique();
        builder.HasIndex(s => s.ExpiresAt);
    }
}
