using maERP.Application.Mediator;
using maERP.Domain.Dtos.TenantEmailSettings;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantEmailSettings.Queries.TenantEmailSettingsDetail;

/// <summary>
/// Returns the email configuration for the current tenant. Returns 404 when no override exists.
/// </summary>
public class TenantEmailSettingsDetailQuery : IRequest<Result<TenantEmailSettingsDetailDto>>
{
}
