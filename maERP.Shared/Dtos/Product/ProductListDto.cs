#nullable disable

using maERP.Shared.Dtos.ProductSalesChannel;

namespace maERP.Shared.Dtos.Product;

public class ProductListDto : ProductBaseDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}