using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.GetSalesChannels;

public record GetSalesChannelsQuery : IRequest<List<GetSalesChannelsResponse>>;