#nullable disable

namespace maERP.Server.Models
{ 
	public class UpdateSalesChannelDto : BaseSalesChannelDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}