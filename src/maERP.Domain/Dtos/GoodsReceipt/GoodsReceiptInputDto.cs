using System.ComponentModel.DataAnnotations;

namespace maERP.Domain.Dtos.GoodsReceipt;

public class GoodsReceiptInputDto
{
    [Required]
    public DateTime ReceiptDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Product is required")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Warehouse is required")]
    public Guid WarehouseId { get; set; }

    [StringLength(255, ErrorMessage = "Supplier cannot exceed 255 characters")]
    public string Supplier { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    public string Notes { get; set; } = string.Empty;
}