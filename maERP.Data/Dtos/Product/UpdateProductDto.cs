#nullable disable

namespace maERP.Data.Dtos.Product
{
	public class UpdateProductDto : BaseProductDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}