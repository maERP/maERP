using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class ChannelExportOutboxConfiguration : IEntityTypeConfiguration<ChannelExportOutbox>
{
    public void Configure(EntityTypeBuilder<ChannelExportOutbox> builder)
    {
        // Drainer's hot path: pick rows that are ready to attempt now.
        builder.HasIndex(e => new { e.Status, e.NextAttemptAt });

        // Idempotency: same logical change cannot enqueue twice for the same channel.
        builder.HasIndex(e => new { e.SalesChannelId, e.IdempotencyKey })
            .IsUnique();

        builder.Property(e => e.IdempotencyKey)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(e => e.LastError).HasMaxLength(2000);

        builder.HasOne(e => e.SalesChannel)
            .WithMany()
            .HasForeignKey(e => e.SalesChannelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
