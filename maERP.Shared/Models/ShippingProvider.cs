using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ShippingProvider : BaseModel
{
    [Required, Display(Name = "Name")]
    public virtual string Name { get; set; } = string.Empty;

    public virtual ICollection<ShippingProviderRate>? ShippingRates { get; set; }
}