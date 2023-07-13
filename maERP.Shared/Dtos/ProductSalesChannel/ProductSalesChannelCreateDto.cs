using System.ComponentModel.DataAnnotations.Schema;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Dtos.SalesChannel;

namespace maERP.Shared.Dtos.ProductSalesChannel;

public class ProductSalesChannelCreateDto
{
    public virtual SalesChannelCreateDto? SalesChannel { get; set; }
    public virtual ProductCreateDto? ProductId { get; set; }

    public int RemoteProductId { get; set; }

    public decimal Price { get; set; }

    public bool ProductImport { get; set; }

    public bool ProductExport { get; set; }
}