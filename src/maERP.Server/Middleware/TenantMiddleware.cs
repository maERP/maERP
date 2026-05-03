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
        var logger = context.RequestServices.GetRequiredService<ILogger<TenantMiddleware>>();
        var path = context.Request.Path.Value;

        logger.LogDebug($"🏢 TenantMiddleware - Processing request: {context.Request.Method} {path}");

        var user = context.User;
        var isTestEnvironment = context.RequestServices.GetService<IWebHostEnvironment>()?.EnvironmentName == "Testing";
        var isAuthenticated = user?.Identity?.IsAuthenticated == true;

        logger.LogDebug($"🔐 TenantMiddleware - IsAuthenticated: {isAuthenticated}, User: {user?.Identity?.Name ?? "null"}");
        logger.LogDebug($"📋 TenantMiddleware - Authorization header: {context.Request.Headers.ContainsKey("Authorization")}");

        // Skip tenant validation for auth endpoints (login, register, forgot-password, reset-password), superadmin endpoints and swagger
        var pathLower = path?.ToLower();
        var isAuthEndpoint = pathLower != null && (pathLower.EndsWith("/auth/login") || pathLower.EndsWith("/auth/register") || pathLower.EndsWith("/auth/forgot-password") || pathLower.EndsWith("/auth/reset-password") || pathLower.EndsWith("/auth/refresh-token"));
        var isSuperadminEndpoint = pathLower != null && pathLower.Contains("/superadmin");
        var isSwaggerEndpoint = pathLower != null && (pathLower.StartsWith("/swagger") || pathLower.StartsWith("/_framework") || pathLower.StartsWith("/_content"));
        var isHealthEndpoint = pathLower != null && pathLower == "/health";
        // server-info is anonymous and tenant-agnostic — the WASM client
        // pings it before login to decide whether to show the registration
        // link. It must not require an X-Tenant-Id header.
        var isServerInfoEndpoint = pathLower != null && pathLower.EndsWith("/server-info");
        // OAuth callback comes from a third-party redirect (eBay / Amazon) and carries no
        // X-Tenant-Id header. Tenant resolution happens via the persisted OAuthState row inside
        // OAuthCallbackHandler (it calls TenantContext.SetCurrentTenantId after state lookup).
        // The /start and /disconnect routes still require the tenant header as usual.
        var isOAuthCallback = pathLower != null && pathLower.Contains("/oauth/") && pathLower.EndsWith("/callback");

        logger.LogDebug($"🔍 TenantMiddleware - isAuthEndpoint: {isAuthEndpoint}, isSuperadminEndpoint: {isSuperadminEndpoint}, isSwaggerEndpoint: {isSwaggerEndpoint}, isOAuthCallback: {isOAuthCallback}");

        if (isAuthEndpoint || isSuperadminEndpoint || isSwaggerEndpoint || isHealthEndpoint || isServerInfoEndpoint || isOAuthCallback)
        {
            logger.LogDebug($"✅  TenantMiddleware - Skipping tenant validation for: {path}");
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
                    if (availableTenantsIds.Count > 0 && !availableTenantsIds.Contains(headerTenantId))
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync($"Access denied: User not assigned to tenant {headerTenantId}");
                        return;
                    }

                    tenantContext.SetCurrentTenantId(headerTenantId);

                    // When JWT has no tenant claims but a valid X-Tenant-Id is provided,
                    // set the assigned tenant IDs so downstream code has consistent context
                    if (availableTenantsIds.Count == 0)
                    {
                        tenantContext.SetAssignedTenantIds(new[] { headerTenantId });
                    }
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
