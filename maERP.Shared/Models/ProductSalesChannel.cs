#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ProductSalesChannel
{
	[Key]
    [Column("id")]
    public int Id { get; set; }

	public virtual SalesChannel SalesChannel { get; set; }

	public virtual Product Product { get; set; }

    [Column("remote_product_id")]
    public int RemoteProductId { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("product_import")]
    public bool ProductImport { get; set; }

    [Column("product_export")]
    public bool ProductExport { get; set; }
}