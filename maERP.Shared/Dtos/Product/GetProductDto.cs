#nullable disable

namespace maERP.Shared.Dtos.Product
{
	public class GetProductDto : BaseProductDto
	{
		public maERP.Shared.Models.TaxClass TaxClass { get; set; }
	}
}