#nullable disable

namespace maERP.Shared.Dtos.Warehouse
{
	public class CreateWarehouseDto : BaseWarehouseDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}