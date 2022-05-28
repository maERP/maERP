#nullable disable

namespace maERP.Data.Dtos
{ 
	public class CreateSalesChannelDto : BaseSalesChannelDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}