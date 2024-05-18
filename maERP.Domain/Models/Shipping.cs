using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class Shipping : BaseEntity
{
    public int OrderId { get; set; }
    public int ShippingProviderId { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string ShippingCost { get; set; } = string.Empty;
    public string ShippingTaxRate { get; set; } = string.Empty;
    public string ShippingProviderName { get; set; } = string.Empty;
}