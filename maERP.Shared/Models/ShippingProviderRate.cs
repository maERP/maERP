 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ShippingProviderRate : ABaseModel
{
    [Required, Display(Name = "Bezeichnung")]
    public virtual string Name { get; set; } = string.Empty;

    [Required, Display(Name = "max. Länge in cm")]
    public virtual decimal MaxLength { get; set; }

    [Required, Display(Name = "max. Breite in cm")]
    public virtual decimal MaxWidth { get; set; }

    [Required, Display(Name = "max. Höhe in cm")]
    public virtual decimal MaxHeight { get; set; }

    [Required, Display(Name = "max. Gewicht in Kg")]
    public virtual decimal MaxWeight { get; set; }
}