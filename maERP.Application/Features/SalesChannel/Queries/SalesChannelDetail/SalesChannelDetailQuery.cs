using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;

public class SalesChannelDetailQuery : IRequest<SalesChannelDetailResponse>
{
    public int Id { get; set; }
}