using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using maERP.Domain.Constants;

namespace maERP.Server.Tests.Infrastructure;

public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions>
{
    public TestAuthenticationHandler(IOptionsMonitor<TestAuthenticationOptions> options,
        ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Check if test should be unauthenticated
        if (Context.Request.Headers.ContainsKey("X-Test-Unauthenticated"))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        // Get tenant configuration from options or use defaults
        var testTenantIds = Options.AssignedTenantIds ?? new[] {
            TenantConstants.TestTenant1Id,
            TenantConstants.TestTenant2Id
        };

        var defaultTenantId = Options.DefaultTenantId ?? testTenantIds.FirstOrDefault();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Options.Username ?? "TestUser"),
            new Claim(ClaimTypes.NameIdentifier, Options.UserId ?? Guid.NewGuid().ToString()),
            new Claim("tenantId", defaultTenantId.ToString()),
            new Claim("availableTenants", JsonSerializer.Serialize(
                testTenantIds.Select(id => new { Id = id.ToString(), Name = $"Test Tenant {id}" })
            ))
        };

        // Add role claims if specified
        if (Options.Roles != null)
        {
            foreach (var role in Options.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}