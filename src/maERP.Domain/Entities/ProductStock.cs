using System.ComponentModel.DataAnnotations;
using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class ProductStock : BaseEntity, IBaseEntity
{
    [Required]
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
    public double Stock { get; set; }
    public double StockMin { get; set; }
    public double StockMax { get; set; }
    public double StorageLocation { get; set; }
}