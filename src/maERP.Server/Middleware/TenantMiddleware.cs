using System.Security.Claims;
using System.Text.Json;
using maERP.Application.Contracts.Services;

namespace maERP.Server.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
    {
        var user = context.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            // Extract available tenants from JWT token
            var availableTenantsIds = ExtractAvailableTenantIds(user);
            tenantContext.SetAssignedTenantIds(availableTenantsIds);

            int? resolvedTenantId = null;

            // Priority 1: Check X-Tenant-Id header (from UI tenant switch)
            if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader))
            {
                if (int.TryParse(tenantHeader.FirstOrDefault(), out var headerTenantId))
                {
                    // SECURITY: Only allow if user is assigned to this tenant
                    if (availableTenantsIds.Contains(headerTenantId))
                    {
                        resolvedTenantId = headerTenantId;
                    }
                    else
                    {
                        // Log security violation
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync($"Access denied: User not assigned to tenant {headerTenantId}");
                        return;
                    }
                }
            }

            // Priority 2: If no header, check JWT token claim (default tenant)
            if (!resolvedTenantId.HasValue)
            {
                var tenantIdClaim = user.FindFirst("tenantId");
                if (tenantIdClaim != null && int.TryParse(tenantIdClaim.Value, out var claimTenantId) && claimTenantId > 0)
                {
                    // SECURITY: Verify default tenant is still assigned to user
                    if (availableTenantsIds.Contains(claimTenantId))
                    {
                        resolvedTenantId = claimTenantId;
                    }
                }
            }

            // Set the validated tenant ID
            if (resolvedTenantId.HasValue)
            {
                tenantContext.SetCurrentTenantId(resolvedTenantId.Value);
            }
        }

        await _next(context);
    }

    private List<int> ExtractAvailableTenantIds(ClaimsPrincipal user)
    {
        var availableTenantsClaim = user.FindFirst("availableTenants");
        if (availableTenantsClaim?.Value != null)
        {
            try
            {
                var tenants = JsonSerializer.Deserialize<List<dynamic>>(availableTenantsClaim.Value);
                return tenants?.Select(t => Convert.ToInt32(((JsonElement)t).GetProperty("Id").GetInt32())).ToList() ?? new List<int>();
            }
            catch
            {
                return new List<int>();
            }
        }
        return new List<int>();
    }
}