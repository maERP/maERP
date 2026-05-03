using maERP.Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace maERP.Server.Services;

/// <summary>
/// Deletes <c>OAuthState</c> rows whose <c>ExpiresAt</c> is older than 1h. Runs every 5 minutes.
/// Keeps the table tiny — used states are also rendered safe by their <c>ConsumedAt</c> single-use
/// flag, but cleanup avoids unbounded growth of attempted-but-abandoned-flow rows.
/// </summary>
public sealed class OAuthStateCleanupService : BackgroundService
{
    private static readonly TimeSpan TickInterval = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan CleanupGracePeriod = TimeSpan.FromHours(1);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OAuthStateCleanupService> _logger;

    public OAuthStateCleanupService(IServiceScopeFactory scopeFactory, ILogger<OAuthStateCleanupService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("OAuthStateCleanupService starting");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await using var scope = _scopeFactory.CreateAsyncScope();
                var repo = scope.ServiceProvider.GetRequiredService<IOAuthStateRepository>();
                var removed = await repo.DeleteExpiredAsync(DateTime.UtcNow - CleanupGracePeriod);
                if (removed > 0)
                {
                    _logger.LogDebug("OAuthStateCleanup removed {Count} expired rows", removed);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OAuthStateCleanup tick failed");
            }

            try
            {
                await Task.Delay(TickInterval, stoppingToken);
            }
            catch (TaskCanceledException) { break; }
        }

        _logger.LogInformation("OAuthStateCleanupService stopping");
    }
}
