#nullable disable

namespace maERP.Shared.Dtos.ProductSalesChannel;

public class UpdateProductSalesChannelDto : BaseProductSalesChannelDto
{
	public DateTime UpdatedAt = DateTime.Now;
	public DateTime CreatedAt = DateTime.Now;
}