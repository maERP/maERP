using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for tax class operations
/// </summary>
public interface ITaxClassesApiClient
{
    /// <summary>
    /// Get paginated list of tax classes
    /// </summary>
    Task<PaginatedResult<TaxClassListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get tax class details by ID
    /// </summary>
    Task<TaxClassDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new tax class
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(TaxClassCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing tax class
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, TaxClassUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a tax class
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
