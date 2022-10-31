#nullable disable

namespace maERP.Shared.Dtos
{ 
	public class CreateSalesChannelDto : BaseSalesChannelDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}