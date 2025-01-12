using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class OrderItem : BaseEntity, IBaseEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public double Quantity { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public double TaxRate { get; set; }
    public string MissingProductSku { get; set; } = string.Empty;
    public string MissingProductEan { get; set; } = string.Empty;
    public int ShippingId { get; set; }
    public ICollection<OrderItemSerialNumber> SerialNumbers { get; set; } = new List<OrderItemSerialNumber>();
}