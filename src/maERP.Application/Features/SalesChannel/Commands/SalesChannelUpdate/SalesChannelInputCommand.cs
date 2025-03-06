using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;

public class SalesChannelInputCommand : SalesChannelInputDto, IRequest<Result<int>>
{
}