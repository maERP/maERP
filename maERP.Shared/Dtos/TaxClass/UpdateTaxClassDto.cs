#nullable disable

namespace maERP.Shared.Dtos.TaxClass
{
	public class UpdateTaxClassDto : BaseTaxClassDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}