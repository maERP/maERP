using maERP.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Entities;

public class GoodsReceipt : BaseEntity, IBaseEntity
{
    [Required]
    public DateTime ReceiptDate { get; set; } = DateTime.Today;

    [Required]
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [Required]
    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    [StringLength(255)]
    public string Supplier { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Notes { get; set; } = string.Empty;

    [StringLength(100)]
    public string CreatedBy { get; set; } = string.Empty;
}