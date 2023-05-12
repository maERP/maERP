using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class TaxClass : ABaseModel
{
    [Required, Display(Name = "Steuersatz in Prozent"), DisplayFormat(NullDisplayText = "19%")]
    public double TaxRate { get; set; }
}