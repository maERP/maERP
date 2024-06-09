using MediatR;

namespace maERP.Application.Features.Order.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<int>
{
    public int Id { get; set; }
    public int SalesChannelId { get; set; }
    public int CustomerId { get; set; }
    public int Status { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;
}