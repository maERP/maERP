#nullable disable

namespace maERP.Shared.Dtos.Product;

public class ProductListDto : ProductBaseDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}