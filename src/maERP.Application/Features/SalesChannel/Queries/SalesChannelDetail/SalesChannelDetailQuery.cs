using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;

/// <summary>
/// Query for retrieving detailed information about a specific sales channel.
/// Implements IRequest to work with MediatR, returning sales channel details wrapped in a Result.
/// </summary>
public class SalesChannelDetailQuery : IRequest<Result<SalesChannelDetailDto>>
{
    /// <summary>
    /// The unique identifier of the sales channel to retrieve
    /// </summary>
    public int Id { get; set; }
}