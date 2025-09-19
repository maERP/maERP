using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TenantEntity = maERP.Domain.Entities.Tenant;
using UserTenantEntity = maERP.Domain.Entities.UserTenant;

namespace maERP.Application.Features.DemoData.Commands.TenantDemoData;

public class TenantDemoDataHandler : IRequestHandler<TenantDemoDataCommand, Result<string>>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public TenantDemoDataHandler(ITenantRepository tenantRepository, IUserTenantRepository userTenantRepository, UserManager<ApplicationUser> userManager)
    {
        _tenantRepository = tenantRepository;
        _userTenantRepository = userTenantRepository;
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(TenantDemoDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if demo tenants already exist
            var existingTenant1 = await _tenantRepository.Entities
                .FirstOrDefaultAsync(t => t.TenantCode == "DEMO1", cancellationToken);
            var existingTenant2 = await _tenantRepository.Entities
                .FirstOrDefaultAsync(t => t.TenantCode == "DEMO2", cancellationToken);

            if (existingTenant1 != null || existingTenant2 != null)
            {
                return Result<string>.Fail("Demo tenants with codes DEMO1 or DEMO2 already exist.");
            }

            // Create first tenant
            var tenant1 = new TenantEntity
            {
                Name = "Demo Tenant 1",
                TenantCode = "DEMO1",
                Description = "First demo tenant for testing multi-tenancy",
                IsActive = true,
                ContactEmail = "demo1@example.com"
            };

            var tenant1Id = await _tenantRepository.CreateAsync(tenant1);

            // Create second tenant
            var tenant2 = new TenantEntity
            {
                Name = "Demo Tenant 2",
                TenantCode = "DEMO2",
                Description = "Second demo tenant for testing multi-tenancy",
                IsActive = true,
                ContactEmail = "demo2@example.com"
            };

            var tenant2Id = await _tenantRepository.CreateAsync(tenant2);

            // Find all existing users
            var usersToAssign = await _userManager.Users.ToListAsync(cancellationToken);

            if (!usersToAssign.Any())
            {
                return Result<string>.Fail("No users found to assign to demo tenants.");
            }

            // Assign all users to ALL tenants (including the newly created demo tenants)
            var allTenantIds = await _tenantRepository.Entities
                .Select(t => t.Id)
                .ToListAsync(cancellationToken);

            foreach (var user in usersToAssign)
            {
                foreach (var tenantId in allTenantIds)
                {
                    var existingUserTenant = await _userTenantRepository.Entities
                        .FirstOrDefaultAsync(ut => ut.UserId == user.Id && ut.TenantId == tenantId, cancellationToken);

                    if (existingUserTenant == null)
                    {
                        var userTenant = new UserTenantEntity
                        {
                            UserId = user.Id,
                            TenantId = tenantId,
                            IsDefault = false,
                            RoleManageUser = false
                        };
                        await _userTenantRepository.CreateAsync(userTenant);
                    }
                }
            }

            var assignedEmails = string.Join(", ", usersToAssign.Select(u => u.Email));
            return Result<string>.Success($"Two demo tenants created. Assigned all users to all tenants: {assignedEmails}");
        }
        catch (Exception ex)
        {
            return Result<string>.Fail($"Failed to create demo tenants: {ex.Message}");
        }
    }
}
