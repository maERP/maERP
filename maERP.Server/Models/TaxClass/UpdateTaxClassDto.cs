#nullable disable

namespace maERP.Server.Models.TaxClass
{
	public class UpdateTaxClassDto : BaseTaxClassDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}