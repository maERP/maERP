namespace maERP.Domain.Dtos.Invoice;

public class InvoiceItemDto
{
    public int Id { get; set; }
    
    public int InvoiceId { get; set; }
    
    public int? ProductId { get; set; }
    
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
} 