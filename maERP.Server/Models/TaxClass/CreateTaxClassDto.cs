#nullable disable

namespace maERP.Server.Models.TaxClass
{
	public class CreateTaxClassDto : BaseTaxClassDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}