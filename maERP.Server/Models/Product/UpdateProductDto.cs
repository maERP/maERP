#nullable disable

namespace maERP.Server.Models.Product
{
	public class UpdateProductDto : BaseProductDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}