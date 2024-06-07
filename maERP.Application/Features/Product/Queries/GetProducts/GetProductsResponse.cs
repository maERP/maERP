using maERP.Application.Features.TaxClass.Queries.GetTaxClasses;

namespace maERP.Application.Features.Product.Queries.GetProducts;

public class GetProductsResponse
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public GetTaxClassesResponse TaxClass { get; set; } = new();
}