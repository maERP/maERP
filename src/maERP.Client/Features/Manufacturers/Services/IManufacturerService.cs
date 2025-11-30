using maERP.Client.Core.Models;
using maERP.Domain.Dtos.Manufacturer;

namespace maERP.Client.Features.Manufacturers.Services;

/// <summary>
/// Service interface for manufacturer-related API operations.
/// </summary>
public interface IManufacturerService
{
    /// <summary>
    /// Gets a paginated list of manufacturers with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<ManufacturerListDto>> GetManufacturersAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a manufacturer by ID.
    /// </summary>
    Task<ManufacturerDetailDto?> GetManufacturerAsync(
        Guid id,
        CancellationToken ct = default);

    /// <summary>
    /// Creates a new manufacturer.
    /// </summary>
    Task CreateManufacturerAsync(
        ManufacturerInputDto input,
        CancellationToken ct = default);

    /// <summary>
    /// Updates an existing manufacturer.
    /// </summary>
    Task UpdateManufacturerAsync(
        Guid id,
        ManufacturerInputDto input,
        CancellationToken ct = default);
}
