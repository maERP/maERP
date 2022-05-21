#nullable disable

using System.ComponentModel.DataAnnotations;

namespace maERP.Server.Data
{
	public class ProductSalesChannel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ProductId { get; set; }

		[Required]
		public int SalesChannelId { get; set; }
	}
}