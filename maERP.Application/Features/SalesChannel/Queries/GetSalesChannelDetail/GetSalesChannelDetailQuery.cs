using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.GetSalesChannelDetail;

public class GetSalesChannelDetailQuery : IRequest<GetSalesChannelDetailResponse>
{
    public int Id { get; set; }
}