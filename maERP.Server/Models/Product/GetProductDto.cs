#nullable disable

namespace maERP.Server.Models.Product
{
	public class GetProductDto : BaseProductDto
	{
		public Data.TaxClass TaxClass { get; set; }
	}
}