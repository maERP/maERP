using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ShippingProvider : BaseModel
{
    public virtual string Name { get; set; } = string.Empty;

    public virtual ICollection<ShippingProviderRate>? ShippingRates { get; set; }
}