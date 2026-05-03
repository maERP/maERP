namespace maERP.Domain.Dtos.SalesChannelOAuth;

/// <summary>
/// Returned by <c>POST /api/v1/saleschannels/{id}/oauth/{provider}/start</c>. The Client opens
/// <see cref="AuthorizeUrl"/> in the system browser; the user logs in at the provider and the
/// browser is redirected back to the Server callback URL with the same <see cref="State"/> token.
/// </summary>
public class OAuthStartResponseDto
{
    public string AuthorizeUrl { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
}
