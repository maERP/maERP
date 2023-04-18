#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ProductSalesChannel
{
	[Key]
    public int Id { get; set; }

	public virtual SalesChannel SalesChannel { get; set; }

	public virtual Product Product { get; set; }

    public int RemoteProductId { get; set; }

    public decimal Price { get; set; }

    public bool ProductImport { get; set; }
]
    public bool ProductExport { get; set; }
}