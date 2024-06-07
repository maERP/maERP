namespace maERP.Application.Features.Order.Queries.GetOrders;

public class GetOrdersResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string InvoiceAddressFirstName { get; set; } = string.Empty;
    public string InvoiceAddressLastName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;

    public DateTime DateOrdered { get; set; }
}