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
}
