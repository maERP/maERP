using maERP.Application.Mediator;
using maERP.Domain.Dtos.TenantEmailSettings;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsUpsert;

/// <summary>
/// Creates or updates the email configuration for the current tenant.
/// Each tenant has at most one configuration; this command upserts that singleton.
/// </summary>
public class TenantEmailSettingsUpsertCommand : TenantEmailSettingsInputDto, IRequest<Result<Guid>>
{
}
