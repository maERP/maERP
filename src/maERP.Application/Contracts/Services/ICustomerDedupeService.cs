using maERP.Domain.Dtos.Customer;

namespace maERP.Application.Contracts.Services;

/// <summary>
/// Cross-tenant customer-deduplication service. Detects (TenantId, lower-cased Email) collisions
/// and, when not in dry-run mode, merges siblings into the oldest (by <c>DateCreated</c>) customer
/// of the group. Implementation lives in the persistence layer because the operation needs direct
/// DbContext access and a transactional sweep across multiple tables.
/// </summary>
public interface ICustomerDedupeService
{
    Task<CustomerDedupeResultDto> RunAsync(bool dryRun, CancellationToken cancellationToken = default);
}
