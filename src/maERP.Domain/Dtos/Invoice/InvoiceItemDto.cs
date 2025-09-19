namespace maERP.Domain.Dtos.Invoice;

public class InvoiceItemDto
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    public Guid? ProductId { get; set; }

    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}