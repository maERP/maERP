#nullable disable

namespace maERP.Shared.Dtos.Product
{
	public class UpdateProductDto : BaseProductDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}