using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class TaxClass : BaseModel
{
    [Required, Display(Name = "Steuersatz in Prozent"), DisplayFormat(NullDisplayText = "19%")]
    public virtual double TaxRate { get; set; }
}