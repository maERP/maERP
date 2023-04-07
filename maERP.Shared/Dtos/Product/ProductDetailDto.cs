#nullable disable

using maERP.Shared.Dtos.ProductSalesChannel;

namespace maERP.Shared.Dtos.Product;

public class ProductDetailDto : ProductBaseDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<ProductSalesChannelDetailDto> ProductSalesChannel { get; set; }
}