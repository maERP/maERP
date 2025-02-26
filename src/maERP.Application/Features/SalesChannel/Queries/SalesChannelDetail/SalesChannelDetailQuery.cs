using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;

public class SalesChannelDetailQuery : IRequest<Result<SalesChannelDetailDto>>
{
    public int Id { get; set; }
}