#nullable disable

namespace maERP.Shared.Dtos.Warehouse
{
	public class UpdateWarehouseDto : BaseWarehouseDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}