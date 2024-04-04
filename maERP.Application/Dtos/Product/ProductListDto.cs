using maERP.Shared.Dtos.TaxClass;

namespace maERP.Shared.Dtos.Product;

public class ProductListDto
{
    public virtual int Id { get; set; }
    public virtual string Sku { get; set; } = string.Empty;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Ean { get; set; } = string.Empty;
    public virtual decimal Price { get; set; }
    public virtual TaxClassListDto TaxClass { get; set; } = new();
}