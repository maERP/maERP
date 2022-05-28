#nullable disable

namespace maERP.Data.Dtos.TaxClass
{
	public class UpdateTaxClassDto : BaseTaxClassDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}