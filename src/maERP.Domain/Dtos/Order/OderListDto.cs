using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Order;

public class OrderListDto
{
    public Guid Id { get; set; }
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string InvoiceAddressFirstName { get; set; } = string.Empty;
    public string InvoiceAddressLastName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime DateOrdered { get; set; }

    public string FullName => $"{InvoiceAddressFirstName} {InvoiceAddressLastName}";
}