using maERP.Application.Features.Manufacturer.Commands.ManufacturerCreate;
using maERP.Application.Features.Manufacturer.Commands.ManufacturerUpdate;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for manufacturer operations
/// </summary>
public interface IManufacturersApiClient
{
    /// <summary>
    /// Get paginated list of manufacturers
    /// </summary>
    Task<PaginatedResult<ManufacturerListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get manufacturer details by ID
    /// </summary>
    Task<ManufacturerDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new manufacturer
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(ManufacturerCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing manufacturer
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, ManufacturerUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a manufacturer
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
