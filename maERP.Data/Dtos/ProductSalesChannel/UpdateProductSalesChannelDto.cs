#nullable disable

namespace maERP.Data.Dtos.ProductSalesChannel;

public class UpdateProductSalesChannelDto : BaseProductSalesChannelDto
{
	public DateTime UpdatedAt = DateTime.Now;
	public DateTime CreatedAt = DateTime.Now;
}