using maERP.Shared.Dtos.ProductSalesChannel;

namespace maERP.Shared.Dtos.Product;

public class ProductDetailDto
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<ProductSalesChannelDetailDto>? ProductSalesChannel { get; set; }
}