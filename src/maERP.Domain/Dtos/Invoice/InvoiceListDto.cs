using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.Invoice;

public class InvoiceListDto
{
    public Guid Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    public Guid? SalesId { get; set; }
    public string SalesNumber { get; set; } = string.Empty;

    public decimal Total { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
    public InvoiceStatus InvoiceStatus { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}