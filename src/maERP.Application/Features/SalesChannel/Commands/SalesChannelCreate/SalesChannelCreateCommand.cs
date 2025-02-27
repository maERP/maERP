using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;

public class SalesChannelCreateCommand : SalesChannelCreateDto, IRequest<Result<int>>
{
}