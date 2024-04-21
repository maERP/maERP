using System.ComponentModel.DataAnnotations;
using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class CustomerAddress : BaseEntity
{
    [Display(Name = "Straße"), DisplayFormat(NullDisplayText = "Straße")]
    public string Street { get; set; } = string.Empty;

    [Display(Name = "Hausnummer"), DisplayFormat(NullDisplayText = "Hausnummer")]
    public string HouseNr { get; set; } = string.Empty;

    [Display(Name = "PLZ"), DisplayFormat(NullDisplayText = "PLZ")]
    public string Zip { get; set; } = string.Empty;

    [Display(Name = "Stadt"), DisplayFormat(NullDisplayText = "Stadt")]
    public string City { get; set; } = string.Empty;

    [Required, Display(Name = "Land")]
    public Country Country { get; set; } = null!;

    [Required]
    public Customer Customer { get; set; } = null!;
}