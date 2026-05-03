using maERP.Application.Mediator;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantOAuthAppSettings.Commands.TenantOAuthAppSettingsUpsert;

public class TenantOAuthAppSettingsUpsertCommand : IRequest<Result<Guid>>
{
    public SalesChannelType Provider { get; set; }
    public bool IsActive { get; set; } = true;
    public string? ClientId { get; set; }

    /// <summary>
    /// <c>null</c> means "keep existing"; non-null means "replace stored value with this".
    /// </summary>
    public string? ClientSecret { get; set; }

    public string? RedirectUri { get; set; }
    public string? RuName { get; set; }
    public string? Scopes { get; set; }
    public bool? UseSandbox { get; set; }
}
