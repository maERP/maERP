using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using maERP.Domain.Common;

namespace maERP.Domain;

public class Country : BaseEntity
{
    [Required, Display(Name = "Name"), DisplayFormat(NullDisplayText = "Name")]
    public string Name { get; set; } = null!;

    [Required, Display(Name = "Ländercode"), DisplayFormat(NullDisplayText = "Ländercode")]
    public string CountryCode { get; set; } = null!;
}