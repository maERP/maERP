#nullable disable

using maERP.Shared.Dtos.ProductSalesChannel;

namespace maERP.Shared.Dtos.Product;

public class ProductDto : BaseProductDto
{ 
    public List<ProductSalesChannelDto> ProductSalesChannel { get; set; }
    public string EAN { get; set; }
}