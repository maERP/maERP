#nullable disable

using System.ComponentModel.DataAnnotations;

namespace maERP.Shared.Models
{
	public class ProductSalesChannel
	{
		[Key]
		public int Id { get; set; }

		// [Required]
		public int SalesChannelId { get; set; }
		public SalesChannel SalesChannel { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }

		public int RemoteProductId { get; set; }

		public decimal Price { get; set; }

		public bool ProductImport { get; set; }
		public bool ProductExport { get; set; }
	}
}