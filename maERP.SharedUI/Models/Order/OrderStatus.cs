namespace maERP.SharedUI.Models.Order;

public enum OrderStatus
{
    Unknown = 0,
    Pending = 1,
    Processing = 2,
    ReadyForDelivery = 3,
    PartiallyDelivered = 4,
    Completed = 5,
    Cancelled = 6,
    Returned = 7,
    Refunded = 8,
    OnHold = 10,
    Failed = 11,
}