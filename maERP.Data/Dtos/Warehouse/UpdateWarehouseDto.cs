#nullable disable

namespace maERP.Data.Dtos.Warehouse
{
	public class UpdateWarehouseDto : BaseWarehouseDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}