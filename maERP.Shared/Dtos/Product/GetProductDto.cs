#nullable disable

namespace maERP.Shared.Dtos.Product
{
	public class GetProductDto : BaseProductDto
	{
		public string EAN { get; set; }
		public maERP.Shared.Models.TaxClass TaxClass { get; set; }
	}
}