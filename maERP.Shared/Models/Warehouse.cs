using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class Warehouse : BaseModel
{
    [Required, Display(Name = "Bezeichnung")]
    public virtual string Name { get; set; } = String.Empty;
}