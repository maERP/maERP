using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;

namespace maERP.Application.Dtos.ProductSalesChannel;

public class ProductSalesChannelCreateDto
{
    public virtual SalesChannelCreateResponse? SalesChannel { get; set; }
    public virtual ProductCreateResponse? ProductId { get; set; }

    public int RemoteProductId { get; set; }

    public decimal Price { get; set; }

    public bool ProductImport { get; set; }

    public bool ProductExport { get; set; }
}