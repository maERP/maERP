using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelList;

public record SalesChannelListQuery : IRequest<List<SalesChannelListResponse>>;