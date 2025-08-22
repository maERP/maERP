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

            // Find users by email addresses
            var adminUser = await _userManager.FindByEmailAsync("admin@localhost.com");
            var regularUser = await _userManager.FindByEmailAsync("user@localhost.com");

            var usersToAssign = new List<ApplicationUser>();
            if (adminUser != null) usersToAssign.Add(adminUser);
            if (regularUser != null) usersToAssign.Add(regularUser);

            if (!usersToAssign.Any())
            {
                return Result<string>.Fail("No users found with emails admin@localhost.com or user@localhost.com");
            }

            // Assign found users to both tenants
            foreach (var user in usersToAssign)
            {
                // Assign to first tenant
                var existingUserTenant1 = await _userTenantRepository.Entities
                    .FirstOrDefaultAsync(ut => ut.UserId == user.Id && ut.TenantId == tenant1Id, cancellationToken);

                if (existingUserTenant1 == null)
                {
                    var userTenant1 = new UserTenantEntity
                    {
                        UserId = user.Id,
                        TenantId = tenant1Id,
                        IsDefault = false
                    };
                    await _userTenantRepository.CreateAsync(userTenant1);
                }

                // Assign to second tenant
                var existingUserTenant2 = await _userTenantRepository.Entities
                    .FirstOrDefaultAsync(ut => ut.UserId == user.Id && ut.TenantId == tenant2Id, cancellationToken);

                if (existingUserTenant2 == null)
                {
                    var userTenant2 = new UserTenantEntity
                    {
                        UserId = user.Id,
                        TenantId = tenant2Id,
                        IsDefault = false
                    };
                    await _userTenantRepository.CreateAsync(userTenant2);
                }
            }

            var assignedEmails = string.Join(", ", usersToAssign.Select(u => u.Email));
            return Result<string>.Success($"Two demo tenants created and assigned to users: {assignedEmails}");
        }
        catch (Exception ex)
        {
            return Result<string>.Fail($"Failed to create demo tenants: {ex.Message}");
        }
    }
}