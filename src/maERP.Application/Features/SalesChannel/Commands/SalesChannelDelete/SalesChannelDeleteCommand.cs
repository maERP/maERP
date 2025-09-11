using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;

public class SalesChannelDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}