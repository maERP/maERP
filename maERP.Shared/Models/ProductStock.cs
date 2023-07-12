using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace maERP.Shared.Models;

public class ProductStock : ABaseModel
{
    public int ProductId { get; set; }

    [Required]
    public Product Product { get; set; } = null!;    

    public int WarehouseId { get; set; }

    [Required]
    public Warehouse Warehouse { get; set; } = null!;

    [Required, Display(Name = "Menge")]
    public int Quantity { get; set; } = 0;
}