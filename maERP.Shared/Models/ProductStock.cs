using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ProductStock : BaseModel
{
    [Required]
    public virtual Product Product { get; set; } = new();

    [Required]
    public virtual Warehouse Warehouse { get; set; } = new();

    [Required]
    public int Quantity { get; set; } = 0;
}