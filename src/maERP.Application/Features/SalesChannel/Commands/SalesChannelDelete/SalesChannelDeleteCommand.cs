using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;

public class SalesChannelDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}