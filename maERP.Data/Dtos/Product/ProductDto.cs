#nullable disable

using maERP.Data.Dtos.ProductSalesChannel;

namespace maERP.Data.Dtos.Product;

public class ProductDto : BaseProductDto
{ 
    public List<ProductSalesChannelDto> ProductSalesChannel { get; set; }
}