using System;
using System.Linq;
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
        Guid[] testTenantIds;
        if (Context.Request.Headers.TryGetValue("X-Test-Tenants", out var tenantHeader) && !string.IsNullOrWhiteSpace(tenantHeader.ToString()))
        {
            var tenantHeaderValue = tenantHeader.ToString();
            testTenantIds = tenantHeaderValue
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(value => Guid.TryParse(value, out var parsed) ? parsed : Guid.Empty)
                .Where(id => id != Guid.Empty)
                .ToArray();

            if (testTenantIds.Length == 0)
            {
                testTenantIds = Options.AssignedTenantIds ?? new[]
                {
                    TenantConstants.TestTenant1Id,
                    TenantConstants.TestTenant2Id
                };
            }
        }
        else
        {
            testTenantIds = Options.AssignedTenantIds ?? new[]
            {
                TenantConstants.TestTenant1Id,
                TenantConstants.TestTenant2Id
            };
        }

        Guid? defaultTenantId = Options.DefaultTenantId;
        if (Context.Request.Headers.TryGetValue("X-Test-DefaultTenantId", out var defaultTenantHeader) && Guid.TryParse(defaultTenantHeader.ToString(), out var parsedDefault) && parsedDefault != Guid.Empty)
        {
            defaultTenantId = parsedDefault;
        }

        defaultTenantId ??= testTenantIds.FirstOrDefault();

        var userId = Options.UserId;
        if (Context.Request.Headers.TryGetValue("X-Test-UserId", out var userIdHeader) && !string.IsNullOrWhiteSpace(userIdHeader))
        {
            userId = userIdHeader!;
        }

        if (string.IsNullOrWhiteSpace(userId))
        {
            userId = Guid.NewGuid().ToString();
        }

        var username = Options.Username;
        if (Context.Request.Headers.TryGetValue("X-Test-Username", out var usernameHeader) && !string.IsNullOrWhiteSpace(usernameHeader))
        {
            username = usernameHeader!;
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            username = "TestUser";
        }

        var effectiveDefaultTenantId = defaultTenantId ?? Guid.Empty;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim("uid", userId),
            new Claim("tenantId", effectiveDefaultTenantId.ToString()),
            new Claim("availableTenants", JsonSerializer.Serialize(
                testTenantIds.Select(id => new { Id = id.ToString(), Name = $"Test Tenant {id}" })
            ))
        };

        var roleClaims = Options.Roles?.ToList() ?? new List<string>();

        if (Context.Request.Headers.TryGetValue("X-Test-Roles", out var headerRolesValue))
        {
            var headerRoles = headerRolesValue.ToString();
            if (!string.IsNullOrWhiteSpace(headerRoles))
            {
                roleClaims = headerRoles
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(role => role.Trim())
                    .Where(role => !string.IsNullOrWhiteSpace(role))
                    .ToList();
            }
            else
            {
                roleClaims = new List<string>();
            }
        }

        foreach (var role in roleClaims)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
