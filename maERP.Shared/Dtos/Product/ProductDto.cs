#nullable disable

using maERP.Shared.Dtos.ProductSalesChannel;

namespace maERP.Shared.Dtos.Product;

public class ProductDto : ProductBaseDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string EAN { get; set; }
    public List<ProductSalesChannelDto> ProductSalesChannel { get; set; }
}