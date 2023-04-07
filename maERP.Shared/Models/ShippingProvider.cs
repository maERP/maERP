
using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Models;

public class ShippingProvider
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    virtual public List<ShippingProviderRate>? ShippingRates { get; set; }
}