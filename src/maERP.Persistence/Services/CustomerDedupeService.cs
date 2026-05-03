using maERP.Application.Contracts.Services;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Services;

/// <summary>
/// Cross-tenant duplicate-customer reconciliation. Always uses <see cref="EntityFrameworkQueryableExtensions.IgnoreQueryFilters{TEntity}"/>
/// because the sweep is by definition cross-tenant; the merge step is still scoped to a single tenant
/// per group (we only collapse customers within the same TenantId).
/// </summary>
public class CustomerDedupeService : ICustomerDedupeService
{
    private readonly ApplicationDbContext _context;

    public CustomerDedupeService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CustomerDedupeResultDto> RunAsync(bool dryRun, CancellationToken cancellationToken = default)
    {
        var result = new CustomerDedupeResultDto { DryRun = dryRun };

        var dupeKeys = await _context.Customer
            .IgnoreQueryFilters()
            .Where(c => c.Email != null && c.Email != string.Empty)
            .Select(c => new { c.TenantId, EmailLower = c.Email.ToLower() })
            .GroupBy(x => new { x.TenantId, x.EmailLower })
            .Where(g => g.Count() > 1)
            .Select(g => new { g.Key.TenantId, g.Key.EmailLower })
            .ToListAsync(cancellationToken);

        if (dupeKeys.Count == 0)
        {
            return result;
        }

        var affectedTenants = new HashSet<Guid>();

        foreach (var key in dupeKeys)
        {
            var members = await _context.Customer
                .IgnoreQueryFilters()
                .Where(c => c.TenantId == key.TenantId && c.Email != null && c.Email.ToLower() == key.EmailLower)
                .OrderBy(c => c.DateCreated)
                .ThenBy(c => c.Id)
                .ToListAsync(cancellationToken);

            if (members.Count < 2)
            {
                continue;
            }

            var survivor = members[0];
            var siblings = members.Skip(1).ToList();
            var siblingIds = siblings.Select(s => s.Id).ToList();
            // Order.CustomerId references Customer.CustomerId (sequential int), not Customer.Id (Guid).
            var siblingCustomerNumbers = siblings.Select(s => s.CustomerId).ToList();

            var addressCount = await _context.CustomerAddress
                .IgnoreQueryFilters()
                .CountAsync(a => siblingIds.Contains(a.CustomerId), cancellationToken);

            var sccCount = await _context.CustomerSalesChannel
                .IgnoreQueryFilters()
                .CountAsync(s => siblingIds.Contains(s.CustomerId), cancellationToken);

            var orderCount = await _context.Order
                .IgnoreQueryFilters()
                .CountAsync(o => siblingCustomerNumbers.Contains(o.CustomerId), cancellationToken);

            var groupDto = new CustomerDedupeGroupDto
            {
                TenantId = key.TenantId ?? Guid.Empty,
                EmailKey = key.EmailLower,
                SurvivorCustomerId = survivor.Id,
                SiblingCustomerIds = siblingIds,
                OrdersReassigned = orderCount,
                AddressesReassigned = addressCount,
                SalesChannelLinksReassigned = sccCount,
            };

            result.Groups.Add(groupDto);
            result.CustomersToMerge += siblings.Count;
            result.OrdersReassigned += orderCount;
            result.AddressesReassigned += addressCount;
            result.SalesChannelLinksReassigned += sccCount;
            if (key.TenantId.HasValue)
            {
                affectedTenants.Add(key.TenantId.Value);
            }

            if (dryRun)
            {
                continue;
            }

            var addresses = await _context.CustomerAddress
                .IgnoreQueryFilters()
                .Where(a => siblingIds.Contains(a.CustomerId))
                .ToListAsync(cancellationToken);
            foreach (var address in addresses)
            {
                address.CustomerId = survivor.Id;
            }

            var sccLinks = await _context.CustomerSalesChannel
                .IgnoreQueryFilters()
                .Where(scc => siblingIds.Contains(scc.CustomerId))
                .ToListAsync(cancellationToken);
            foreach (var link in sccLinks)
            {
                link.CustomerId = survivor.Id;
            }

            var orders = await _context.Order
                .IgnoreQueryFilters()
                .Where(o => siblingCustomerNumbers.Contains(o.CustomerId))
                .ToListAsync(cancellationToken);
            foreach (var order in orders)
            {
                order.CustomerId = survivor.CustomerId;
            }

            var mergedAt = DateTime.UtcNow;

            // Canonicalize survivor's email to lowercase so the upcoming filtered unique index
            // (PR 5) does not see mixed-case duplicates after the merge.
            if (!string.IsNullOrEmpty(survivor.Email))
            {
                survivor.Email = survivor.Email.Trim().ToLowerInvariant();
            }

            foreach (var sibling in siblings)
            {
                sibling.CustomerStatus = CustomerStatus.Inactive;
                sibling.Email = string.Empty;
                sibling.Note = (string.IsNullOrEmpty(sibling.Note) ? string.Empty : sibling.Note + " ")
                    + $"[Merged into {survivor.Id:D} on {mergedAt:O}]";
                sibling.DateModified = mergedAt;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        result.GroupsFound = result.Groups.Count;
        result.TenantsAffected = affectedTenants.Count;

        return result;
    }
}
