using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Sales;

public class SalesListDto
{
    public Guid Id { get; set; }
    public int SalesId { get; set; }
    public int CustomerId { get; set; }
    public string InvoiceAddressFirstName { get; set; } = string.Empty;
    public string InvoiceAddressLastName { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public SalesStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime DateSalesed { get; set; }

    public string FullName => $"{InvoiceAddressFirstName} {InvoiceAddressLastName}";
}