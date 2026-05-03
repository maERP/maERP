using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Orchestration;

/// <summary>
/// Fan-out helper used by notification handlers: takes a domain change and enqueues one
/// ChannelExportOutbox row per (SalesChannel × aggregate) tuple that needs to know about it.
///
/// IdempotencyKey is stable per (Operation, AggregateType, AggregateId, SalesChannelId) — so
/// rapid-fire updates to the same aggregate coalesce into a single Pending row instead of
/// piling up in the queue. The drainer (PR 12) hydrates the payload from the current DB state
/// at dispatch time, so a coalesced row always carries the latest data.
/// </summary>
public sealed class ChannelExportOutboxEnqueuer
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ChannelExportOutboxEnqueuer> _logger;

    public ChannelExportOutboxEnqueuer(ApplicationDbContext context, ILogger<ChannelExportOutboxEnqueuer> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>Enqueue one outbox row per channel that should receive this change.</summary>
    public async Task EnqueueAsync(
        IReadOnlyList<Guid> salesChannelIds,
        ChannelSyncOperation operation,
        ChannelOutboxAggregateType aggregateType,
        Guid aggregateId,
        Guid? tenantId,
        CancellationToken cancellationToken = default)
    {
        if (salesChannelIds.Count == 0)
        {
            return;
        }

        var now = DateTime.UtcNow;

        foreach (var salesChannelId in salesChannelIds)
        {
            var key = BuildIdempotencyKey(operation, aggregateType, aggregateId, salesChannelId);

            var existing = await _context.ChannelExportOutbox
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => o.SalesChannelId == salesChannelId && o.IdempotencyKey == key, cancellationToken);

            if (existing is null)
            {
                _context.ChannelExportOutbox.Add(new ChannelExportOutbox
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    SalesChannelId = salesChannelId,
                    Operation = operation,
                    AggregateType = aggregateType,
                    AggregateId = aggregateId,
                    PayloadJson = string.Empty, // Drainer hydrates from current DB state.
                    IdempotencyKey = key,
                    AttemptCount = 0,
                    NextAttemptAt = now,
                    Status = ChannelOutboxStatus.Pending,
                });
                continue;
            }

            switch (existing.Status)
            {
                case ChannelOutboxStatus.Pending:
                    existing.NextAttemptAt = now;
                    break;

                case ChannelOutboxStatus.InFlight:
                    // Drainer is mid-flight; it'll re-read state. Nothing to do.
                    break;

                case ChannelOutboxStatus.Done:
                case ChannelOutboxStatus.DeadLetter:
                    existing.Status = ChannelOutboxStatus.Pending;
                    existing.AttemptCount = 0;
                    existing.NextAttemptAt = now;
                    existing.LastError = null;
                    existing.CompletedAt = null;
                    break;
            }
        }

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            // Concurrent enqueue from another notification path produced the same key —
            // safe to swallow; the existing row already represents the change we wanted.
            _logger.LogDebug(ex,
                "Outbox enqueue race for op={Op} agg={Type}:{Id} — existing row will carry the change.",
                operation, aggregateType, aggregateId);
        }
    }

    public static string BuildIdempotencyKey(
        ChannelSyncOperation operation,
        ChannelOutboxAggregateType aggregateType,
        Guid aggregateId,
        Guid salesChannelId)
        => $"{(int)operation}:{(int)aggregateType}:{aggregateId:N}:{salesChannelId:N}";
}
