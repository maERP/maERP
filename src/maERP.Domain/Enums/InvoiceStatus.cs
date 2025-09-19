namespace maERP.Domain.Enums;

public enum InvoiceStatus
{
    Unknown = 0,
    Created = 1,
    Sent = 2,
    Overdue = 3,
    PartiallyPaid = 4,
    Paid = 5,
    Cancelled = 6,
    Disputed = 7,
    Refunded = 8,
    WrittenOff = 9,
    Archived = 10
}