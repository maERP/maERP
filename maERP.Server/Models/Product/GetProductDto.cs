#nullable disable

using maERP.Server.Data;

namespace maERP.Server.Models
{
	public class GetProductDto : BaseProductDto
	{
		public TaxClass TaxClass { get; set; }
	}
}