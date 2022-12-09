#nullable disable

using maERP.Shared.Dtos.TaxClass;

namespace maERP.Shared.Dtos.Product;

public class ProductBaseDto
{
    public int Id { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public TaxClassDto TaxClass { get; set; }
}