using maERP.Application.Dtos.Product;
using maERP.Application.Dtos.SalesChannel;

namespace maERP.Application.Dtos.ProductSalesChannel;

public class ProductSalesChannelCreateDto
{
    public virtual SalesChannelCreateDto? SalesChannel { get; set; }
    public virtual ProductCreateDto? ProductId { get; set; }

    public int RemoteProductId { get; set; }

    public decimal Price { get; set; }

    public bool ProductImport { get; set; }

    public bool ProductExport { get; set; }
}