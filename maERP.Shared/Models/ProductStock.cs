#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ProductStock
{
	[Key]
    public int Id { get; set; }

	[Required]
    public virtual Product Product { get; set; }

    [Required]
    public virtual Warehouse Warehouse { get; set; }

    [Required]
    public int Quantity { get; set; }
}