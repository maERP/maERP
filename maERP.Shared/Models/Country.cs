using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Country : BaseModel
{
    [Display(Name = "Name")]
    public virtual string Name { get; set; } = null!;

    [Display(Name = "Ländercode")]
    public virtual string CountryCode { get; set; } = null!;
}