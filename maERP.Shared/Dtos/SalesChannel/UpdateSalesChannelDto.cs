#nullable disable

namespace maERP.Shared.Dtos
{ 
	public class UpdateSalesChannelDto : BaseSalesChannelDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}