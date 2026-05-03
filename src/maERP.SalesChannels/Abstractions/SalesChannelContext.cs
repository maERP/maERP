using System.Net.Http;
using maERP.Domain.Entities;

namespace maERP.SalesChannels.Abstractions;

/// <summary>
/// Per-run context handed to a connector. Stateless connectors read everything they need from
/// here — credentials are already decrypted, the typed HttpClient already has Polly + auth set up,
/// and the audit row (<see cref="ChannelSyncRun"/>) is open and waiting for ItemsProcessed updates.
/// </summary>
public sealed class SalesChannelContext
{
    public required SalesChannel SalesChannel { get; init; }

    /// <summary>Decrypted Password (legacy field). Empty for OAuth-only channels.</summary>
    public string Password { get; init; } = string.Empty;

    /// <summary>Decrypted current OAuth access token. May be null/expired — connector should refresh as needed.</summary>
    public string? AccessToken { get; init; }

    /// <summary>Decrypted refresh token. Long-lived; rotated when the channel issues a new one.</summary>
    public string? RefreshToken { get; init; }

    /// <summary>HttpClient pre-configured with Polly retry/circuit-breaker and the channel's base URL.</summary>
    public required HttpClient HttpClient { get; init; }

    /// <summary>Sync-run audit row this dispatch logs against. Connector writes ItemsProcessed/Failed.</summary>
    public required ChannelSyncRun SyncRun { get; init; }

    public Guid? TenantId { get; init; }

    public CancellationToken CancellationToken { get; init; }
}
