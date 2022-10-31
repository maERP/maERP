#nullable disable

namespace maERP.Shared.Dtos.TaxClass
{
	public class CreateTaxClassDto : BaseTaxClassDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}