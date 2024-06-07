namespace maERP.Application.Features.Order.Commands.UpdateOrder;

public class UpdateOrderResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;
}