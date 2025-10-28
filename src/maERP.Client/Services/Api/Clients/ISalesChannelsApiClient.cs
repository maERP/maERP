using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for sales channel operations
/// </summary>
public interface ISalesChannelsApiClient
{
    /// <summary>
    /// Get paginated list of sales channels
    /// </summary>
    Task<PaginatedResult<SalesChannelListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get sales channel details by ID
    /// </summary>
    Task<SalesChannelDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new sales channel
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(SalesChannelCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing sales channel
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, SalesChannelUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a sales channel
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
