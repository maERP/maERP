using System.Security.Claims;
using System.Text.Json;
using maERP.Application.Contracts.Services;
using Microsoft.AspNetCore.Hosting;

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
        var isTestEnvironment = context.RequestServices.GetService<IWebHostEnvironment>()?.EnvironmentName == "Testing";

        // Check if X-Tenant-Id header is present
        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader))
        {
            if (int.TryParse(tenantHeader.FirstOrDefault(), out var headerTenantId))
            {
                // In test environment, always honor the X-Tenant-Id header
                if (isTestEnvironment)
                {
                    tenantContext.SetCurrentTenantId(headerTenantId);
                }
                // In production, handle authenticated vs non-authenticated scenarios
                else if (user.Identity?.IsAuthenticated != true)
                {
                    // For unauthenticated requests, set tenant ID directly
                    tenantContext.SetCurrentTenantId(headerTenantId);
                }
                else
                {
                    // For authenticated requests, validate tenant access
                    var availableTenantsIds = ExtractAvailableTenantIds(user);
                    tenantContext.SetAssignedTenantIds(availableTenantsIds);

                    if (availableTenantsIds.Contains(headerTenantId))
                    {
                        tenantContext.SetCurrentTenantId(headerTenantId);
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
            else
            {
                // X-Tenant-Id header present but not parseable as integer
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("X-Tenant-Id header must be a valid integer");
                return;
            }
        }
        else
        {
            // For authenticated users without X-Tenant-Id header, use default tenant from JWT
            if (user.Identity?.IsAuthenticated == true)
            {
                var availableTenantsIds = ExtractAvailableTenantIds(user);
                tenantContext.SetAssignedTenantIds(availableTenantsIds);

                var tenantIdClaim = user.FindFirst("tenantId");
                if (tenantIdClaim != null && int.TryParse(tenantIdClaim.Value, out var claimTenantId) && claimTenantId > 0)
                {
                    // SECURITY: Verify default tenant is still assigned to user
                    if (availableTenantsIds.Contains(claimTenantId))
                    {
                        tenantContext.SetCurrentTenantId(claimTenantId);
                    }
                }
            }
            else
            {
                // For unauthenticated requests without X-Tenant-Id header, return Unauthorized
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("X-Tenant-Id header is required for this request");
                return;
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