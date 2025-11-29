using maERP.Client.Core.Models;
using maERP.Domain.Dtos.TaxClass;

namespace maERP.Client.Features.TaxClasses.Services;

/// <summary>
/// Service interface for tax class-related API operations.
/// </summary>
public interface ITaxClassService
{
    /// <summary>
    /// Gets a paginated list of tax classes with full pagination metadata.
    /// </summary>
    Task<PaginatedResponse<TaxClassListDto>> GetTaxClassesAsync(
        QueryParameters parameters,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single tax class by ID.
    /// </summary>
    Task<TaxClassDetailDto?> GetTaxClassAsync(Guid id, CancellationToken ct = default);
}
