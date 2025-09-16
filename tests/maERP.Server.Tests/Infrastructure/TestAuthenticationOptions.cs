using Microsoft.AspNetCore.Authentication;

namespace maERP.Server.Tests.Infrastructure;

public class TestAuthenticationOptions : AuthenticationSchemeOptions
{
    public Guid[]? AssignedTenantIds { get; set; }
    public Guid? DefaultTenantId { get; set; }
    public string? Username { get; set; }
    public string? UserId { get; set; }
    public string[]? Roles { get; set; }
    public bool IsAuthenticated { get; set; } = true;
}