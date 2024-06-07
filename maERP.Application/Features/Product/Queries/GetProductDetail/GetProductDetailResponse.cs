using maERP.Application.Dtos.ProductSalesChannel;

namespace maERP.Application.Features.Product.Queries.GetProductDetail;

public class GetProductDetailResponse
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<ProductSalesChannelDetailDto>? ProductSalesChannel { get; set; }
}