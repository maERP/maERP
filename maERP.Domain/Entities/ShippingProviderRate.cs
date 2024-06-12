using System.ComponentModel.DataAnnotations;
using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class ShippingProviderRate : BaseEntity, IBaseEntity
{
    [Required, Display(Name = "Bezeichnung")]
    public string Name { get; set; } = string.Empty;

    [Required, Display(Name = "max. Länge in cm")]
    public decimal MaxLength { get; set; }

    [Required, Display(Name = "max. Breite in cm")]
    public decimal MaxWidth { get; set; }

    [Required, Display(Name = "max. Höhe in cm")]
    public decimal MaxHeight { get; set; }

    [Required, Display(Name = "max. Gewicht in Kg")]
    public decimal MaxWeight { get; set; }
}