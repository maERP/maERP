using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class SalesChannel : BaseEntity, IBaseEntity
{
    public SalesChannelType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    /// <summary>Encrypted at rest via <c>EncryptedStringConverter</c>.</summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>OAuth access token (encrypted at rest). Refreshed by the connector before each call when expired.</summary>
    public string? AccessToken { get; set; }

    /// <summary>OAuth refresh token (encrypted at rest). Persisted across server restarts.</summary>
    public string? RefreshToken { get; set; }

    /// <summary>UTC expiry of the current access token. Connector refreshes when within ~60 seconds of this.</summary>
    public DateTime? TokenExpiresAt { get; set; }

    /// <summary>
    /// Channel-specific marketplace identifier (eBay marketplace like <c>EBAY_DE</c>, Amazon marketplace id, ...).
    /// Single SalesChannel = single marketplace; multi-marketplace sellers create one row per marketplace.
    /// </summary>
    public string? MarketplaceId { get; set; }

    /// <summary>Free-form connector configuration (sandbox flag, policy IDs, seller id, ...). Schema is owned by the connector.</summary>
    public string? AdditionalConfigJson { get; set; }

    public bool ImportProducts { get; set; }
    public bool ImportCustomers { get; set; }
    public bool ImportOrders { get; set; }
    public bool ExportProducts { get; set; }
    public bool ExportCustomers { get; set; }
    public bool ExportOrders { get; set; }
    public bool InitialProductImportCompleted { get; set; }
    public bool InitialProductExportCompleted { get; set; }

    /// <summary>Polling interval used by the orchestrator. Defaults to 60s.</summary>
    public int SyncIntervalSeconds { get; set; } = 60;

    /// <summary>UTC time of the last sync attempt — orchestrator schedules the next dispatch from this.</summary>
    public DateTime? LastSyncStartedAt { get; set; }

    /// <summary>Kill-switch independent of the per-direction Import/Export flags.</summary>
    public bool IsEnabled { get; set; } = true;

    public ICollection<Warehouse> Warehouses { get; set; } = null!;
}
