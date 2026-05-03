using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Orchestration;

/// <summary>
/// Single hosted service that drives all sales-channel work: schedules import polling per
/// channel (using <c>SyncIntervalSeconds</c> + <c>LastSyncStartedAt</c>) and drains the
/// export outbox. Replaces every per-channel <c>IHostedService</c> in <c>Tasks/</c>.
/// </summary>
public sealed class SalesChannelOrchestrator : BackgroundService
{
    private static readonly TimeSpan TickInterval = TimeSpan.FromSeconds(10);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SalesChannelOrchestrator> _logger;

    public SalesChannelOrchestrator(IServiceScopeFactory scopeFactory, ILogger<SalesChannelOrchestrator> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SalesChannelOrchestrator starting");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await DrainOutboxAsync(stoppingToken);
                await PollImportsAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Orchestrator tick failed");
            }

            try
            {
                await Task.Delay(TickInterval, stoppingToken);
            }
            catch (TaskCanceledException) { break; }
        }

        _logger.LogInformation("SalesChannelOrchestrator stopping");
    }

    private async Task DrainOutboxAsync(CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var drainer = scope.ServiceProvider.GetRequiredService<OutboxDrainer>();
        var processed = await drainer.DrainOnceAsync(cancellationToken);
        if (processed > 0)
        {
            _logger.LogDebug("Drainer processed {Count} outbox rows", processed);
        }
    }

    private async Task PollImportsAsync(CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var dispatcher = scope.ServiceProvider.GetRequiredService<SyncDispatcher>();

        var now = DateTime.UtcNow;
        // Provider-portable filter: pull enabled channels and gate in-memory by elapsed seconds
        // (EF.Functions.DateDiffSecond is SQL-Server-only and EF Core cannot translate
        // (now - LastSyncStartedAt).TotalSeconds across providers).
        var enabledChannels = await context.SalesChannel
            .IgnoreQueryFilters()
            .Where(s => s.IsEnabled)
            .ToListAsync(cancellationToken);

        var dueChannels = enabledChannels
            .Where(s => s.LastSyncStartedAt is null
                        || (now - s.LastSyncStartedAt.Value).TotalSeconds >= s.SyncIntervalSeconds)
            .ToList();

        if (dueChannels.Count == 0)
        {
            return;
        }

        foreach (var channel in dueChannels)
        {
            cancellationToken.ThrowIfCancellationRequested();

            channel.LastSyncStartedAt = now;
            await context.SaveChangesAsync(cancellationToken);

            try
            {
                if (channel.ImportProducts)
                {
                    await dispatcher.RunImportAsync(channel, ChannelSyncOperation.ImportProducts, ChannelSyncTriggerSource.Scheduler, cancellationToken);
                }
                if (channel.ImportOrders)
                {
                    await dispatcher.RunImportAsync(channel, ChannelSyncOperation.ImportOrders, ChannelSyncTriggerSource.Scheduler, cancellationToken);
                }
                if (channel.ImportCustomers)
                {
                    await dispatcher.RunImportAsync(channel, ChannelSyncOperation.ImportCustomers, ChannelSyncTriggerSource.Scheduler, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Import-poll failed for channel {ChannelId} ({Name})", channel.Id, channel.Name);
            }
        }
    }
}
