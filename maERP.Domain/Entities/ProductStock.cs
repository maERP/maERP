using maERP.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Models;

public class ProductStock : BaseEntity, IBaseEntity
{
    [Required]
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    public double Stock { get; set; } = 0;
}