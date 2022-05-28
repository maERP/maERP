#nullable disable

namespace maERP.Data.Dtos.Product
{
	public class GetProductDto : BaseProductDto
	{
		public Data.Models.TaxClass TaxClass { get; set; }
	}
}