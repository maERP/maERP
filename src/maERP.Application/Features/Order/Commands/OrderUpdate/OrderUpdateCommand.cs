using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderUpdate;

public class OrderUpdateCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public int SalesChannelId { get; set; }
    public int CustomerId { get; set; }
    public int Status { get; set; }
    public string RemoteOrderId { get; set; } = string.Empty;
}