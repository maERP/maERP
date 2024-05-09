using maERP.Application.Dtos.TaxClass;

namespace maERP.Application.Dtos.Product;

public class ProductListDto
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TaxClassListDto TaxClass { get; set; } = new();
}