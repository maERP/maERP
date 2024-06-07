using maERP.Application.Dtos.ProductSalesChannel;
using maERP.Application.Features.TaxClass.Queries.GetTaxClasses;

namespace maERP.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductResponse
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public string Asin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Msrp { get; set; }
    public GetTaxClassesResponse? TaxClass { get; set; }
    public List<ProductSalesChannelDetailDto>? ProductSalesChannel { get; set; }
}