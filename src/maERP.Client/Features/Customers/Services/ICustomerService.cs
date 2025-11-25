using System.Collections.Immutable;
using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Services;

/// <summary>
/// Service interface for customer-related API operations.
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Gets a paginated list of customers.
    /// </summary>
    Task<IImmutableList<CustomerListDto>> GetCustomersAsync(
        int page = 0,
        int pageSize = 20,
        string? searchQuery = null,
        CancellationToken ct = default);

    /// <summary>
    /// Gets a single customer by ID.
    /// </summary>
    Task<CustomerDetailDto?> GetCustomerAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    Task<CustomerDetailDto> CreateCustomerAsync(CustomerInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    Task<CustomerDetailDto> UpdateCustomerAsync(Guid id, CustomerInputDto input, CancellationToken ct = default);

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    Task DeleteCustomerAsync(Guid id, CancellationToken ct = default);
}
