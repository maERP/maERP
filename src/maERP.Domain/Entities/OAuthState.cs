using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

/// <summary>
/// Short-lived state-token store for the OAuth Authorization-Code flow. Each row maps a single
/// <see cref="StateToken"/> back to its originating <c>SalesChannel</c> + <c>Tenant</c> + provider
/// when the third-party redirect comes back to the callback endpoint (which carries no auth
/// context of its own).
///
/// Single-use: <see cref="ConsumedAt"/> is set before the token-exchange call, so a replayed
/// callback request fails fast. Cleanup runs on a 5-minute tick deleting rows past expiry+1h.
/// </summary>
public class OAuthState : BaseEntityWithoutTenant, IBaseEntityWithoutTenant
{
    [Required]
    public Guid TenantId { get; set; }

    [Required]
    public Guid SalesChannelId { get; set; }

    [Required]
    public SalesChannelType Provider { get; set; }

    /// <summary>base64url(32 random bytes) — the value sent to the provider as <c>state</c>.</summary>
    [Required]
    [MaxLength(128)]
    public string StateToken { get; set; } = string.Empty;

    /// <summary>Random server-side nonce; logged for diagnostics, not exposed in the URL.</summary>
    [Required]
    [MaxLength(128)]
    public string Nonce { get; set; } = string.Empty;

    /// <summary>UTC. Defaults to <c>CreatedAt + 10 min</c>.</summary>
    [Required]
    public DateTime ExpiresAt { get; set; }

    /// <summary>UTC. Set before the code → token exchange to enforce single-use.</summary>
    public DateTime? ConsumedAt { get; set; }

    [MaxLength(64)]
    public string? CreatedByUserId { get; set; }
}
