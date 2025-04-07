using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Order;

public class OrderHistoryDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Action { get; set; } = string.Empty;
    public OrderStatus? PreviousStatus { get; set; }
    public OrderStatus? NewStatus { get; set; }
    public PaymentStatus? PreviousPaymentStatus { get; set; }
    public PaymentStatus? NewPaymentStatus { get; set; }
    public string? PreviousShippingStatus { get; set; }
    public string? NewShippingStatus { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public bool IsSystemGenerated { get; set; }
} 