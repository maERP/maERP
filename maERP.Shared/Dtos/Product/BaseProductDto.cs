#nullable disable

using maERP.Shared.Dtos.TaxClass;

namespace maERP.Shared.Dtos.Product;

public class ProductBaseDto
{
    public int Id { get; set; }
    public string SKU { get; set; }
    public string EAN { get; set; }
    public TaxClassDetailDto TaxClass { get; set; }
}