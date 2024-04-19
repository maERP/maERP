using maERP.Application.Dtos.SalesChannel;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.GetSalesChannelDetailQuery;

public class GetSalesChannelDetailQuery : IRequest<SalesChannelDetailDto>
{
    public int Id { get; set; }
}