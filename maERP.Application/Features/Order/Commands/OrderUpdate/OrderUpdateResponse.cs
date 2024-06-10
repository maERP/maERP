namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;
}