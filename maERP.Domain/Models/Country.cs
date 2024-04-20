using System.ComponentModel.DataAnnotations;
using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class Country : BaseEntity
{
    [Required, Display(Name = "Name"), DisplayFormat(NullDisplayText = "Name")]
    public string Name { get; set; } = null!;

    [Required, Display(Name = "Ländercode"), DisplayFormat(NullDisplayText = "Ländercode")]
    public string CountryCode { get; set; } = null!;
}