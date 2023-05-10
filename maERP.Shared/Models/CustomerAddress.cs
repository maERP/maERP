using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace maERP.Shared.Models;

public class CustomerAddress : BaseModel
{
    [Display(Name = "Straße"), DisplayFormat(NullDisplayText = "Straße")]
    public virtual string Street { get; set; } = String.Empty;

    [Display(Name = "Hausnummer"), DisplayFormat(NullDisplayText = "Hausnummer")]
    public virtual string HouseNr { get; set; } = String.Empty;

    [Display(Name = "PLZ"), DisplayFormat(NullDisplayText = "PLZ")]
    public virtual string Zip { get; set; } = String.Empty;

    [Display(Name = "Stadt"), DisplayFormat(NullDisplayText = "Stadt")]
    public virtual string City { get; set; } = String.Empty;

    [Required, Display(Name = "Land")]
    public virtual Country Country { get; set; } = null!;

    [Required]
    public virtual Customer Customer { get; set; } = null!;
}