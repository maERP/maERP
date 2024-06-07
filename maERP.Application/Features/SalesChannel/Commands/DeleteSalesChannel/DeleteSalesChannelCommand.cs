using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannel;

public class DeleteSalesChannelCommand : IRequest<int>
{
    public int Id { get; set; }     
}