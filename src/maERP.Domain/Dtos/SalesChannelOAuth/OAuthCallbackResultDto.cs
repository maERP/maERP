using maERP.Domain.Enums;

namespace maERP.Domain.Dtos.SalesChannelOAuth;

/// <summary>
/// Internal handler result for the OAuth callback. The controller renders this as a small HTML
/// page (the callback is hit by the browser, not by an API client).
/// </summary>
public class OAuthCallbackResultDto
{
    public Guid SalesChannelId { get; set; }
    public Guid TenantId { get; set; }
    public SalesChannelType Provider { get; set; }
    public DateTime? TokenExpiresAt { get; set; }
}
