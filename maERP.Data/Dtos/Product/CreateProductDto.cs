#nullable disable

namespace maERP.Data.Dtos.Product
{
	public class CreateProductDto : BaseProductDto
	{
		public string Description { get; set; }
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}