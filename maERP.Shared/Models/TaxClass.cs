using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class TaxClass : BaseModel
{
    [Required]
    public virtual double TaxRate { get; set; }
}