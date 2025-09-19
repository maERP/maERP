using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Tests.Infrastructure;

public static class CurrentUserHelper
{
    public const string DefaultUserId = "11111111-1111-1111-1111-111111111111";
    private const string DefaultEmail = "manager@test.com";

    public static async Task<string> EnsureUserAsync(HttpClient client, ApplicationDbContext context, Guid tenantId, bool canManageUsers)
    {
        var userId = DefaultUserId;

        client.DefaultRequestHeaders.Remove("X-Test-UserId");
        client.DefaultRequestHeaders.Add("X-Test-UserId", userId);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            user = new ApplicationUser
            {
                Id = userId,
                Email = DefaultEmail,
                NormalizedEmail = DefaultEmail.ToUpperInvariant(),
                UserName = DefaultEmail,
                NormalizedUserName = DefaultEmail.ToUpperInvariant(),
                EmailConfirmed = true,
                Firstname = "Test",
                Lastname = "Manager",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            context.Users.Add(user);
        }

        var assignment = await context.UserTenant.FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TenantId == tenantId);
        if (assignment == null)
        {
            assignment = new UserTenant
            {
                UserId = userId,
                TenantId = tenantId,
                IsDefault = true,
                RoleManageUser = canManageUsers,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            context.UserTenant.Add(assignment);
        }
        else
        {
            assignment.RoleManageUser = canManageUsers;
            assignment.IsDefault = true;
            assignment.DateModified = DateTime.UtcNow;
            context.UserTenant.Update(assignment);
        }

        await context.SaveChangesAsync();

        return userId;
    }

    private const string TenantsHeader = "X-Test-Tenants";

    public static async Task SyncAssignmentsAsync(HttpClient client, ApplicationDbContext context, IEnumerable<Guid> tenantIds, bool canManageUsers, Guid defaultTenantId)
    {
        var targetArray = (tenantIds ?? Array.Empty<Guid>()).Where(id => id != Guid.Empty).Distinct().ToArray();
        var targetSet = targetArray.ToHashSet();

        var assignments = await context.UserTenant
            .Where(ut => ut.UserId == DefaultUserId)
            .ToListAsync();

        var toRemove = assignments.Where(ut => !targetSet.Contains(ut.TenantId)).ToList();
        if (toRemove.Count > 0)
        {
            context.UserTenant.RemoveRange(toRemove);
        }

        foreach (var tenantId in targetSet)
        {
            var assignment = assignments.FirstOrDefault(ut => ut.TenantId == tenantId);
            if (assignment == null)
            {
                assignment = new UserTenant
                {
                    UserId = DefaultUserId,
                    TenantId = tenantId,
                    DateCreated = DateTime.UtcNow
                };

                context.UserTenant.Add(assignment);
            }

            assignment.RoleManageUser = canManageUsers;
            assignment.IsDefault = tenantId == defaultTenantId;
            assignment.DateModified = DateTime.UtcNow;
        }

        client.DefaultRequestHeaders.Remove(TenantsHeader);
        client.DefaultRequestHeaders.Remove("X-Test-DefaultTenantId");
        if (targetArray.Length > 0)
        {
            client.DefaultRequestHeaders.Add(TenantsHeader, string.Join(',', targetArray));
            client.DefaultRequestHeaders.Add("X-Test-DefaultTenantId", defaultTenantId.ToString());
        }

        await context.SaveChangesAsync();
    }
}
