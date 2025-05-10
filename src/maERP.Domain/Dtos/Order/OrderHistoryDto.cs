using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Order;

public class OrderHistoryDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public string Action { get; set; } = string.Empty;
    public OrderStatus? OrderStatusOld { get; set; }
    public OrderStatus? OrderStatusNew { get; set; }
    public PaymentStatus? PaymentStatusOld { get; set; }
    public PaymentStatus? PaymentStatusNew { get; set; }
    public string? ShippingStatusOld { get; set; }
    public string? ShippingStatusNew { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsSystemGenerated { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}