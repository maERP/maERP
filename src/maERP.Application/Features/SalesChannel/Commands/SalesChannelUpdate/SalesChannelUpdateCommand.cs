using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;

public class SalesChannelUpdateCommand : SalesChannelUpdateDto, IRequest<Result<int>>
{
}