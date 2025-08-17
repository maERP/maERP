using System.Security.Claims;
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
            var tenantIdClaim = user.FindFirst("tenantId");
            if (tenantIdClaim != null && int.TryParse(tenantIdClaim.Value, out var tenantId))
            {
                tenantContext.SetCurrentTenantId(tenantId);
            }
        }

        await _next(context);
    }
}