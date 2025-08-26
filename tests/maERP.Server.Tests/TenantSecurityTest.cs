using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests;

public class TenantSecurityTest
{
    [Fact]
    public async Task TenantMiddleware_ShouldBlock_UnauthorizedTenantAccess()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var tenantContext = new TenantContext();
        
        // Setup user with access only to tenant 1
        var claims = new List<Claim>
        {
            new Claim("uid", "user123"),
            new Claim("tenantId", "1"),
            new Claim("availableTenants", JsonSerializer.Serialize(new[]
            {
                new { Id = 1, Name = "Tenant 1", TenantCode = "T1" }
            }))
        };
        context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));
        
        // Try to access tenant 2 (unauthorized)
        context.Request.Headers["X-Tenant-Id"] = "2";
        
        var middleware = new maERP.Server.Middleware.TenantMiddleware((ctx) => 
        {
            // This should not be reached if security works
            Assert.Fail("Middleware should have blocked unauthorized access");
            return Task.CompletedTask;
        });

        // Act & Assert
        await middleware.InvokeAsync(context, tenantContext);
        
        // Should return 403 Forbidden
        Assert.Equal(403, context.Response.StatusCode);
    }

    [Fact]
    public async Task TenantMiddleware_ShouldAllow_AuthorizedTenantAccess()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var tenantContext = new TenantContext();
        var nextCalled = false;
        
        // Setup user with access to tenant 1 and 2
        var claims = new List<Claim>
        {
            new Claim("uid", "user123"),
            new Claim("tenantId", "1"),
            new Claim("availableTenants", JsonSerializer.Serialize(new[]
            {
                new { Id = 1, Name = "Tenant 1", TenantCode = "T1" },
                new { Id = 2, Name = "Tenant 2", TenantCode = "T2" }
            }))
        };
        context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));
        
        // Try to access tenant 2 (authorized)
        context.Request.Headers["X-Tenant-Id"] = "2";
        
        var middleware = new maERP.Server.Middleware.TenantMiddleware((ctx) => 
        {
            nextCalled = true;
            return Task.CompletedTask;
        });

        // Act
        await middleware.InvokeAsync(context, tenantContext);
        
        // Assert
        Assert.True(nextCalled, "Next middleware should have been called");
        Assert.Equal(2, tenantContext.GetCurrentTenantId());
        Assert.Contains(2, tenantContext.GetAssignedTenantIds());
    }

    [Fact]
    public void TenantContext_ShouldReject_UnassignedTenant()
    {
        // Arrange
        var tenantContext = new TenantContext();
        tenantContext.SetAssignedTenantIds(new[] { 1, 2 }); // User assigned to tenant 1 and 2
        
        // Act - try to set tenant 3 (not assigned)
        tenantContext.SetCurrentTenantId(3);
        
        // Assert
        Assert.Null(tenantContext.GetCurrentTenantId()); // Should remain null
    }

    [Fact]
    public void TenantContext_ShouldAccept_AssignedTenant()
    {
        // Arrange
        var tenantContext = new TenantContext();
        tenantContext.SetAssignedTenantIds(new[] { 1, 2 }); // User assigned to tenant 1 and 2
        
        // Act - set tenant 2 (assigned)
        tenantContext.SetCurrentTenantId(2);
        
        // Assert
        Assert.Equal(2, tenantContext.GetCurrentTenantId());
    }
}