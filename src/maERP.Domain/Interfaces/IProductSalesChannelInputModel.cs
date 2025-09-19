namespace maERP.Domain.Interfaces;

public interface IProductSalesChannelInputModel
{
    Guid SalesChannelId { get; }
    Guid ProductId { get; }
    Guid RemoteProductId { get; }
    decimal Price { get; }
}
