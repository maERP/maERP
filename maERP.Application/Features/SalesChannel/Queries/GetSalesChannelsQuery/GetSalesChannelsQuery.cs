using maERP.Application.Dtos.SalesChannel;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.GetSalesChannelsQuery;

public record GetSalesChannelsQuery : IRequest<List<SalesChannelListDto>>;