namespace maERP.SharedUI.Models.Order;

public class OrderListVM
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string InvoiceAddressFirstName { get; set; } = string.Empty;
    public string InvoiceAddressLastName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DateOrdered { get; set; }

    public string FullName => $"{InvoiceAddressFirstName} {InvoiceAddressLastName}";
}