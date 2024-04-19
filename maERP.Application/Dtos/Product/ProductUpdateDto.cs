using maERP.Application.Dtos.ProductSalesChannel;
using maERP.Application.Dtos.TaxClass;

namespace maERP.Application.Dtos.Product;

public class ProductUpdateDto
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public string Asin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Msrp { get; set; }
    public TaxClassListDto? TaxClass { get; set; }
    public List<ProductSalesChannelDetailDto>? ProductSalesChannel { get; set; }
}