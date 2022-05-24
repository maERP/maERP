#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Server.Data
{
	public class ProductStock
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("Product")]
		public int ProductId { get; set; }

		[Required]
		public int WarehouseId { get; set; }

		[Required]
		public int Quantity { get; set; }

		public virtual IList<Product> Products { get; set; }
	}
}