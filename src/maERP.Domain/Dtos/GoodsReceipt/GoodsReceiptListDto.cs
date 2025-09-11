namespace maERP.Domain.Dtos.GoodsReceipt;

public class GoodsReceiptListDto
{
    public Guid Id { get; set; }
    public DateTime ReceiptDate { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductSku { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string Supplier { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
}