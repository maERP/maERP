#nullable disable

namespace maERP.Data.Dtos.ProductSalesChannel;

public class BaseProductSalesChannelDto
{
    public int Id { get; set; }
    public int RemoteProductId { get; set; }
    public decimal Price { get; set; }
}