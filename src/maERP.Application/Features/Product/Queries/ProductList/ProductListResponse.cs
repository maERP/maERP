using maERP.Application.Features.TaxClass.Queries.TaxClassList;

namespace maERP.Application.Features.Product.Queries.ProductList;

public class ProductListResponse
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TaxClassListResponse TaxClass { get; set; } = new();
}