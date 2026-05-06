using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Orchestration;

/// <summary>
/// One drainer pass: pick up to <see cref="BatchSize"/> Pending outbox rows that are due
/// (NextAttemptAt &lt;= now), set them InFlight, dispatch via <see cref="SyncDispatcher"/>,
/// then mark Done or schedule the next retry with exponential backoff. After 10 failed
/// attempts a row lands in <see cref="ChannelOutboxStatus.DeadLetter"/>.
/// </summary>
public sealed class OutboxDrainer
{
    private const int BatchSize = 100;
    private const int MaxAttempts = 10;

    private readonly ApplicationDbContext _context;
    private readonly SyncDispatcher _dispatcher;
    private readonly ILogger<OutboxDrainer> _logger;

    public OutboxDrainer(ApplicationDbContext context, SyncDispatcher dispatcher, ILogger<OutboxDrainer> logger)
    {
        _context = context;
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public async Task<int> DrainOnceAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var due = await _context.ChannelExportOutbox
            .IgnoreQueryFilters()
            .Where(o => o.Status == ChannelOutboxStatus.Pending && o.NextAttemptAt <= now)
            .OrderBy(o => o.NextAttemptAt)
            .Take(BatchSize)
            .ToListAsync(cancellationToken);

        if (due.Count == 0)
        {
            return 0;
        }

        var processed = 0;

        foreach (var outbox in due)
        {
            cancellationToken.ThrowIfCancellationRequested();

            outbox.Status = ChannelOutboxStatus.InFlight;
            outbox.AttemptCount++;
            await _context.SaveChangesAsync(cancellationToken);

            var salesChannel = await _context.SalesChannel
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.Id == outbox.SalesChannelId, cancellationToken);

            if (salesChannel is null || !salesChannel.IsEnabled)
            {
                MarkFailed(outbox, "SalesChannel missing or disabled");
                if (outbox.AttemptCount >= MaxAttempts) outbox.Status = ChannelOutboxStatus.DeadLetter;
                else outbox.Status = ChannelOutboxStatus.Pending;
                outbox.NextAttemptAt = ScheduleRetry(outbox.AttemptCount);
                await _context.SaveChangesAsync(cancellationToken);
                continue;
            }

            var result = await _dispatcher.RunExportAsync(salesChannel, outbox, cancellationToken);

            if (result.Success)
            {
                outbox.Status = ChannelOutboxStatus.Done;
                outbox.CompletedAt = DateTime.UtcNow;
                outbox.LastError = null;
            }
            else
            {
                MarkFailed(outbox, result.ErrorMessage ?? "unknown");
                if (outbox.AttemptCount >= MaxAttempts)
                {
                    outbox.Status = ChannelOutboxStatus.DeadLetter;
                    _logger.LogWarning(
                        "Outbox row {Id} for channel {Channel} reached max attempts — moved to DeadLetter",
                        outbox.Id, outbox.SalesChannelId);
                }
                else
                {
                    outbox.Status = ChannelOutboxStatus.Pending;
                    outbox.NextAttemptAt = ScheduleRetry(outbox.AttemptCount);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            processed++;
        }

        return processed;
    }

    private static DateTime ScheduleRetry(int attemptCount)
    {
        // Exponential backoff capped at 1h: 30s, 60s, 120s, 240s, ..., max 3600s.
        var seconds = Math.Min(3600d, 30d * Math.Pow(2, attemptCount - 1));
        return DateTime.UtcNow.AddSeconds(seconds);
    }

    private static void MarkFailed(ChannelExportOutbox outbox, string error)
    {
        outbox.LastError = error.Length > 2000 ? error[..2000] : error;
    }
}
