#nullable disable

using maERP.Shared.Dtos.ProductSalesChannel;

namespace maERP.Shared.Dtos.Product;

public class ProductListDto : ProductBaseDto
{ 
    public List<ProductSalesChannelDto> ProductSalesChannel { get; set; }
    public string EAN { get; set; }
}