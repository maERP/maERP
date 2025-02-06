namespace maERP.Domain.Dtos.ProductSalesChannel;

public class ProductSalesChannelCreateDto
{
    public int RemoteProductId { get; set; }

    public decimal Price { get; set; }

    public bool ProductImport { get; set; }

    public bool ProductExport { get; set; }
}