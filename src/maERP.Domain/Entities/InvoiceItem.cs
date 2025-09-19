using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class InvoiceItem : BaseEntity, IBaseEntity
{
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }

    public double Quantity { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string SKU { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public double TaxRate { get; set; }
    public decimal TaxAmount { get; set; }

    public string Unit { get; set; } = "piece";

    public Guid? OrderItemId { get; set; }
}
