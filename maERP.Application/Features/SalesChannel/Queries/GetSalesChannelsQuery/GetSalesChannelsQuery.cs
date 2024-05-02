using maERP.Application.Dtos.SalesChannel;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.GetAllSalesChannelsQuery;

public record GetSalesChannelsQuery : IRequest<List<SalesChannelListDto>>;