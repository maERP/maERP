using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class Shipping : BaseEntity, IBaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ShippingProviderId { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string ShippingCost { get; set; } = string.Empty;
    public string ShippingTaxRate { get; set; } = string.Empty;
    public string ShippingProviderName { get; set; } = string.Empty;
}