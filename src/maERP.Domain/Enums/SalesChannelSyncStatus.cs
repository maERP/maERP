namespace maERP.Domain.Enums;

/// <summary>
/// Sync state of a single <c>ProductSalesChannel</c> row vis-à-vis its remote channel.
/// </summary>
public enum SalesChannelSyncStatus
{
    Pending = 0,
    Synced = 1,
    Error = 2,
    Disabled = 3
}
