namespace maERP.Domain.Interfaces;

public interface IProductSalesChannelInputModel
{
    int SalesChannelId { get; }
    int ProductId { get; }
    int RemoteProductId { get; }
    decimal Price { get; }
}
