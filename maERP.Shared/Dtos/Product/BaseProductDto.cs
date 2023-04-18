using maERP.Shared.Dtos.TaxClass;

namespace maERP.Shared.Dtos.Product;

public class ProductBaseDto
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;
    public TaxClassDetailDto TaxClass { get; set; }
}