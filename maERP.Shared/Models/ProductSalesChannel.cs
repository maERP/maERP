using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maERP.Shared.Models;

public class ProductSalesChannel : BaseModel
{
	public virtual SalesChannel SalesChannel { get; set; }

	public virtual Product Product { get; set; }

    public virtual int RemoteProductId { get; set; }

    public virtual decimal Price { get; set; }

    public virtual bool ProductImport { get; set; }

    public virtual bool ProductExport { get; set; }
}