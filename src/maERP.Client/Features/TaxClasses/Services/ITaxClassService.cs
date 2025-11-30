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

    /// <summary>
    /// Creates a new tax class.
    /// </summary>
    Task CreateTaxClassAsync(TaxClassInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing tax class.
    /// </summary>
    Task UpdateTaxClassAsync(Guid id, TaxClassInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Deletes a tax class.
    /// </summary>
    Task DeleteTaxClassAsync(Guid id, CancellationToken ct = default);
}
