namespace maERP.Domain.Enums;

/// <summary>
/// What a single sync run / outbox row represents. Used both by <c>ChannelSyncRun</c>
/// (audit log) and <c>ChannelExportOutbox</c> (export queue).
/// </summary>
public enum ChannelSyncOperation
{
    ImportProducts = 1,
    ImportSaless = 2,
    ImportCustomers = 3,
    ExportProduct = 10,
    UpdateStock = 11,
    UpdatePrice = 12,
    UpdateSales = 13,
    DelistProduct = 14,
}
