using maERP.Client.Core.Models;
using maERP.Domain.Dtos.SalesChannel;

namespace maERP.Client.Features.SalesChannels.Services;

/// <summary>
/// Service interface for sales channel-related API operations.
/// </summary>
public interface ISalesChannelService
{
    /// <summary>
    /// Gets a paginated list of sales channels with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<SalesChannelListDto>> GetSalesChannelsAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single sales channel by ID.
    /// </summary>
    Task<SalesChannelDetailDto?> GetSalesChannelAsync(Guid id, CancellationToken ct = default);
}
