namespace maERP.Domain.Enums;

/// <summary>
/// Bitmask of operations a connector supports. The orchestrator AND-combines this with the
/// SalesChannel's per-direction Import/Export flags before dispatching, so a connector that
/// does not implement (e.g.) <see cref="UpdatePrice"/> never receives that work item.
/// </summary>
[Flags]
public enum SalesChannelCapabilities
{
    None = 0,
    ImportProducts = 1,
    ImportSaless = 1 << 1,
    ImportCustomers = 1 << 2,
    ExportProducts = 1 << 3,
    UpdateStock = 1 << 4,
    UpdatePrice = 1 << 5,
    UpdateSaless = 1 << 6,
    DelistProducts = 1 << 7,
    OAuth = 1 << 8,
    RequiresMarketplaceId = 1 << 9,
}
