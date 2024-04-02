using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models.Database;

public class Country : BaseModel
{
    [Required, Display(Name = "Name"), DisplayFormat(NullDisplayText = "Name")]
    public string Name { get; set; } = null!;

    [Required, Display(Name = "Ländercode"), DisplayFormat(NullDisplayText = "Ländercode")]
    public string CountryCode { get; set; } = null!;
}