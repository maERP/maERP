#nullable disable

namespace maERP.Server.Models
{
	public class UpdateProductDto : BaseProductDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}