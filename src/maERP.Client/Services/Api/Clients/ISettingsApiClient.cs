using maERP.Application.Features.Setting.Commands.SettingCreate;
using maERP.Application.Features.Setting.Commands.SettingUpdate;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for setting operations
/// </summary>
public interface ISettingsApiClient
{
    /// <summary>
    /// Get paginated list of settings
    /// </summary>
    Task<PaginatedResult<SettingListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get setting details by ID
    /// </summary>
    Task<SettingDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new setting
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(SettingCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing setting
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, SettingUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a setting
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
