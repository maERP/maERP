using maERP.Shared.Dtos.ProductSalesChannel;

namespace maERP.Shared.Dtos.Product;

public class ProductUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<ProductSalesChannelDetailDto>? ProductSalesChannel { get; set; }
}