using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace maERP.Shared.Models;

public class ProductStock : ABaseModel
{
    public uint ProductId { get; set; }

    [Required]
    public Product Product { get; set; } = null!;    

    public uint WarehouseId { get; set; }

    [Required]
    public Warehouse Warehouse { get; set; } = null!;

    [Required, Display(Name = "Menge")]
    public uint Quantity { get; set; } = 0;
}