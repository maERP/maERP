namespace maERP.Domain.Models;

/// <summary>
/// Represents the status of a shipping process.
/// </summary>
public enum ShippingStatus
{
    Open = 1,
    InProgess = 2,
    ReadyForShipping = 3,
    Shipped = 4,
    Delivered = 5,
    Cancelled = 6
}