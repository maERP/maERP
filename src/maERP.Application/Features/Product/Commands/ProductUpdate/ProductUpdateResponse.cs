using maERP.Application.Features.TaxClass.Queries.TaxClassList;
using maERP.Domain.Dtos.ProductSalesChannel;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateResponse
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public string Asin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Msrp { get; set; }
    public TaxClassListResponse? TaxClass { get; set; }
    public List<ProductSalesChannelDetailDto>? ProductSalesChannel { get; set; }
}