namespace maERP.Domain.Dtos.GoodsReceipt;

public class GoodsReceiptDetailDto
{
    public int Id { get; set; }
    public DateTime ReceiptDate { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductSku { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string Supplier { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}