using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using maERP.Domain.Common;

namespace maERP.Domain;

public class ProductStock : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int WarehouseId { get; set; }

    public Warehouse Warehouse { get; set; } = null!;
    public int Quantity { get; set; } = 0;
}