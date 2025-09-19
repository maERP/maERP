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
        var isAuthenticated = user?.Identity?.IsAuthenticated == true;

        // Skip tenant validation for auth endpoints (login, register)
        var path = context.Request.Path.Value?.ToLower();
        var isAuthEndpoint = path != null && (path.EndsWith("/auth/login") || path.EndsWith("/auth/register"));
        if (isAuthEndpoint)
        {
            await _next(context);
            return;
        }

        var availableTenantsIds = new List<Guid>();
        if (isAuthenticated && user != null)
        {
            availableTenantsIds = ExtractAvailableTenantIds(user);
            if (availableTenantsIds.Count > 0)
            {
                tenantContext.SetAssignedTenantIds(availableTenantsIds);
            }
        }

        Guid? requestedTenantId = null;
        if (context.Request.Headers.TryGetValue("X-Tenant-Id", out var tenantHeader))
        {
            var tenantHeaderValue = tenantHeader.FirstOrDefault();
            if (tenantHeaderValue != null && Guid.TryParse(tenantHeaderValue, out var headerTenantId) && headerTenantId != Guid.Empty)
            {
                requestedTenantId = headerTenantId;

                if (isTestEnvironment)
                {
                    tenantContext.SetCurrentTenantId(headerTenantId);
                }
                else if (!isAuthenticated)
                {
                    tenantContext.SetCurrentTenantId(headerTenantId);
                }
                else
                {
                    if (!availableTenantsIds.Contains(headerTenantId))
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync($"Access denied: User not assigned to tenant {headerTenantId}");
                        return;
                    }

                    tenantContext.SetCurrentTenantId(headerTenantId);
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid X-Tenant-Id header format");
                return;
            }
        }
        else
        {
            if (isTestEnvironment)
            {
                Guid? defaultTenantId = null;
                if (context.Request.Headers.TryGetValue("X-Test-DefaultTenantId", out var defaultTenantHeader) && Guid.TryParse(defaultTenantHeader.FirstOrDefault(), out var parsedDefault) && parsedDefault != Guid.Empty)
                {
                    defaultTenantId = parsedDefault;
                }

                if (defaultTenantId.HasValue)
                {
                    tenantContext.SetCurrentTenantId(defaultTenantId.Value);
                }
                else
                {
                    tenantContext.SetCurrentTenantId(Guid.Empty);
                }
            }
            else if (isAuthenticated)
            {
                var tenantIdClaim = user?.FindFirst("tenantId");
                if (tenantIdClaim != null && Guid.TryParse(tenantIdClaim.Value, out var claimTenantId) && claimTenantId != Guid.Empty && availableTenantsIds.Contains(claimTenantId))
                {
                    tenantContext.SetCurrentTenantId(claimTenantId);
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("X-Tenant-Id header is required for this request");
                return;
            }
        }

        if (isAuthenticated && availableTenantsIds.Count == 0 && requestedTenantId.HasValue)
        {
            tenantContext.SetAssignedTenantIds(new[] { requestedTenantId.Value });
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
