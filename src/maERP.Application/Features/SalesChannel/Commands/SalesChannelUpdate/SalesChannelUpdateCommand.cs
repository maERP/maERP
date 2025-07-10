using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;

public class SalesChannelUpdateCommand : SalesChannelInputDto, IRequest<Result<int>>
{
}