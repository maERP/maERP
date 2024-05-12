using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal TaxRate { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public int ShippingId { get; set; } 
}