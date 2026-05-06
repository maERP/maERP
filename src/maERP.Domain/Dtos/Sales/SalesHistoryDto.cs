using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Sales;

public class SalesHistoryDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SalesId { get; set; }
    public string Action { get; set; } = string.Empty;
    public SalesStatus? SalesStatusOld { get; set; }
    public SalesStatus? SalesStatusNew { get; set; }
    public PaymentStatus? PaymentStatusOld { get; set; }
    public PaymentStatus? PaymentStatusNew { get; set; }
    public string? ShippingStatusOld { get; set; }
    public string? ShippingStatusNew { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsSystemGenerated { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}