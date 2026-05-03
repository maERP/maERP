namespace maERP.Domain.Interfaces;

public interface IProductSalesChannelInputModel
{
    Guid SalesChannelId { get; }
    Guid ProductId { get; }
    string? RemoteProductId { get; }
    decimal Price { get; }
}
