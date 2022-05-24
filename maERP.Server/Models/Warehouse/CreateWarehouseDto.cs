#nullable disable

namespace maERP.Server.Models.Warehouse
{
	public class CreateWarehouseDto : BaseWarehouseDto
	{
		public DateTime UpdatedAt = DateTime.Now;
		public DateTime CreatedAt = DateTime.Now;
	}
}