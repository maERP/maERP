using maERP.Client.Core.Models;
using maERP.Domain.Dtos.SalesChannel;

namespace maERP.Client.Features.SalesChannels.Services;

/// <summary>
/// Service interface for sales channel-related API operations.
/// </summary>
public interface ISalesChannelService
{
    /// <summary>
    /// Gets a paginated list of sales channels with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<SalesChannelListDto>> GetSalesChannelsAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single sales channel by ID.
    /// </summary>
    Task<SalesChannelDetailDto?> GetSalesChannelAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new sales channel.
    /// </summary>
    Task CreateSalesChannelAsync(SalesChannelInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing sales channel.
    /// </summary>
    Task UpdateSalesChannelAsync(Guid id, SalesChannelInputDto input, CancellationToken ct = default);

    /// <summary>Trigger a manual sync (operation = "products" | "orders" | "customers" | "all").</summary>
    Task<SalesChannelSyncResultDto?> TriggerSyncAsync(Guid id, string operation, CancellationToken ct = default);

    /// <summary>Test the channel's credentials/connectivity without doing any import.</summary>
    Task<SalesChannelSyncResultDto?> TestConnectionAsync(Guid id, CancellationToken ct = default);

    /// <summary>Recent sync-run audit log for the channel.</summary>
    Task<List<ChannelSyncRunDto>> GetSyncRunsAsync(Guid id, int take = 50, int offset = 0, CancellationToken ct = default);

    /// <summary>Outbox rows currently in DeadLetter for the channel.</summary>
    Task<List<ChannelExportOutboxDto>> GetDeadLetterAsync(Guid id, CancellationToken ct = default);

    /// <summary>Reset a DeadLetter outbox row back to Pending so the drainer retries it.</summary>
    Task RetryDeadLetterAsync(Guid id, Guid outboxId, CancellationToken ct = default);

    /// <summary>Begin OAuth flow — returns the authorize URL the Client should open in the system browser.</summary>
    Task<OAuthStartResult> StartOAuthAsync(Guid id, string provider, CancellationToken ct = default);

    /// <summary>Disconnect OAuth — clears stored refresh / access tokens on the channel.</summary>
    Task DisconnectOAuthAsync(Guid id, string provider, CancellationToken ct = default);
}

/// <summary>Lightweight DTO for the OAuth-start response (kept here to avoid a Domain dependency in the existing JsonContext bindings).</summary>
public sealed record OAuthStartResult(string AuthorizeUrl, string State);
