using maERP.Application.Features.Product.Commands.CreateProduct;
using maERP.Application.Features.SalesChannel.Commands.CreateSalesChannel;

namespace maERP.Application.Dtos.ProductSalesChannel;

public class ProductSalesChannelCreateDto
{
    public virtual CreateSalesChannelResponse? SalesChannel { get; set; }
    public virtual CreateProductResponse? ProductId { get; set; }

    public int RemoteProductId { get; set; }

    public decimal Price { get; set; }

    public bool ProductImport { get; set; }

    public bool ProductExport { get; set; }
}