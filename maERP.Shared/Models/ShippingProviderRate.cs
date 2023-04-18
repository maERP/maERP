using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ShippingProviderRate : BaseModel
{
    public virtual string Name { get; set; } = string.Empty;

    public virtual decimal MaxLength { get; set; }

    public virtual decimal MaxWidth { get; set; }

    public virtual decimal MaxHeight { get; set; }

    public virtual decimal MaxWeight { get; set; }
}