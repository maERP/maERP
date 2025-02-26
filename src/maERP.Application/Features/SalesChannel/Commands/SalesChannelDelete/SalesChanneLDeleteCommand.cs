using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;

public class SalesChanneLDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }     
}