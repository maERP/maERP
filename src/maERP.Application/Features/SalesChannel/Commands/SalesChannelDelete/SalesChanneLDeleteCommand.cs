using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;

public class SalesChanneLDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}