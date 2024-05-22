using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public double Quantity { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public double TaxRate { get; set; }
    public int ShippingId { get; set; }
    public ICollection<OrderItemSerialNumber> SerialNumbers { get; set; } = new List<OrderItemSerialNumber>();
}