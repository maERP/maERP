using maERP.Application.Mediator;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SystemOAuthSettings.Commands.SystemOAuthSettingsUpsert;

public class SystemOAuthSettingsUpsertCommand : IRequest<Result<int>>
{
    public SalesChannelType Provider { get; set; }
    public string? ClientId { get; set; }

    /// <summary><c>null</c> = keep stored ciphertext; non-null = replace.</summary>
    public string? ClientSecret { get; set; }

    public string? RuName { get; set; }
    public string? RedirectUri { get; set; }
    public string? AuthorizationEndpoint { get; set; }
    public string? TokenEndpoint { get; set; }
    public string? Scopes { get; set; }
    public bool UseSandbox { get; set; }
    public string? PublicBaseUrl { get; set; }
}
