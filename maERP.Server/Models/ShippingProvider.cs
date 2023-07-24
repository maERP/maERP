using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Server.Models;

public class ShippingProvider : ABaseModel
{
    [Required, Display(Name = "Name")]
    public virtual string Name { get; set; } = string.Empty;

    public virtual ICollection<ShippingProviderRate>? ShippingRates { get; set; }
}