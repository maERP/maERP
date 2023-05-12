using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Country : ABaseModel
{
    [Required, Display(Name = "Name"), DisplayFormat(NullDisplayText = "Name")]
    public virtual string Name { get; set; } = null!;

    [Required, Display(Name = "Ländercode"), DisplayFormat(NullDisplayText = "Ländercode")]
    public virtual string CountryCode { get; set; } = null!;
}