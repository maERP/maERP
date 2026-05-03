using maERP.Application.Mediator;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.Customer.Commands.CustomerDedupe;

/// <summary>
/// Cross-tenant customer-dedupe command. With <see cref="DryRun"/> = true, no writes happen —
/// the response describes what *would* be merged. With <see cref="DryRun"/> = false, the merge
/// is applied: per (TenantId, lower-cased Email) collision the oldest <c>DateCreated</c>
/// customer survives and absorbs siblings' addresses, sales-channel links, and orders.
/// Siblings get <c>CustomerStatus.Inactive</c> and their email cleared so the unique index
/// (added in PR 5) does not block them.
/// </summary>
public class CustomerDedupeCommand : IRequest<Result<CustomerDedupeResultDto>>
{
    public bool DryRun { get; set; } = true;
}
