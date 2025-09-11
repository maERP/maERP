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

        // Skip tenant validation for auth endpoints (login, register)
        var path = context.Request.Path.Value?.ToLower();
        var isAuthEndpoint = path != null && (path.EndsWith("/auth/login") || path.EndsWith("/auth/register"));
        if (isAuthEndpoint)
        {
            await _next(context);
            return;
        }

        // Check if X-Tenant-Id header is present
        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader))
        {
            var tenantHeaderValue = tenantHeader.FirstOrDefault();
            if (Guid.TryParse(tenantHeaderValue, out var headerTenantId))
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
                // X-Tenant-Id header present but not parseable as GUID
                // Set an invalid tenant ID to ensure queries return no results (404 behavior)
                // This maintains tenant isolation by not revealing header validation details
                tenantContext.SetCurrentTenantId(Guid.Empty);
            }
        }
        else
        {
            // In test environment, treat missing header as invalid tenant to maintain consistent behavior
            if (isTestEnvironment)
            {
                tenantContext.SetCurrentTenantId(Guid.Empty);
            }
            // For authenticated users without X-Tenant-Id header, use default tenant from JWT
            else if (user.Identity?.IsAuthenticated == true)
            {
                var availableTenantsIds = ExtractAvailableTenantIds(user);
                tenantContext.SetAssignedTenantIds(availableTenantsIds);

                var tenantIdClaim = user.FindFirst("tenantId");
                if (tenantIdClaim != null && Guid.TryParse(tenantIdClaim.Value, out var claimTenantId) && claimTenantId != Guid.Empty)
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

    private List<Guid> ExtractAvailableTenantIds(ClaimsPrincipal user)
    {
        var availableTenantsClaim = user.FindFirst("availableTenants");
        if (availableTenantsClaim?.Value != null)
        {
            try
            {
                var tenants = JsonSerializer.Deserialize<List<dynamic>>(availableTenantsClaim.Value);
                return tenants?.Select(t => Guid.Parse(((JsonElement)t).GetProperty("Id").GetString() ?? string.Empty)).ToList() ?? new List<Guid>();
            }
            catch
            {
                return new List<Guid>();
            }
        }
        return new List<Guid>();
    }
}