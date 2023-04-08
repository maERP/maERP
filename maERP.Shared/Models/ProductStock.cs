#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ProductStock
{
	[Key]
    [Column("id")]
    public int Id { get; set; }

	[Required]
    [Column("product_id")]
    public virtual Product Product { get; set; }

    [Required]
    public virtual Warehouse Warehouse { get; set; }

    [Required]
    [Column("quantity")]
    public int Quantity { get; set; }
}