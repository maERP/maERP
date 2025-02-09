using maERP.Domain.Dtos.SalesChannel;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;

public class SalesChannelDetailQuery : IRequest<SalesChannelDetailDto>
{
    public int Id { get; set; }
}