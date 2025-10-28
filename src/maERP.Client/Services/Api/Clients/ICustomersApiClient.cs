using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for customer operations
/// </summary>
public interface ICustomersApiClient
{
    /// <summary>
    /// Get paginated list of customers
    /// </summary>
    Task<PaginatedResult<CustomerListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get customer details by ID
    /// </summary>
    Task<CustomerDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new customer
    /// </summary>
    Task<CustomerDetailDto?> CreateAsync(CustomerCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing customer
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, CustomerUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a customer
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
