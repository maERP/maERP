using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maERP.Persistence.Configurations;

public class ChannelSyncRunConfiguration : IEntityTypeConfiguration<ChannelSyncRun>
{
    public void Configure(EntityTypeBuilder<ChannelSyncRun> builder)
    {
        builder.HasIndex(e => new { e.SalesChannelId, e.StartedAt })
            .IsDescending(false, true);

        builder.HasIndex(e => e.CorrelationId);

        builder.Property(e => e.ErrorSummary).HasMaxLength(2000);

        builder.HasOne(e => e.SalesChannel)
            .WithMany()
            .HasForeignKey(e => e.SalesChannelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
