using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannelCommand;

public class DeleteSalesChannelCommand : IRequest<int>
{
    public int Id { get; set; }     
}