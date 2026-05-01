using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsDelete;

/// <summary>
/// Removes the tenant-level email override so the tenant falls back to the server defaults.
/// </summary>
public class TenantEmailSettingsDeleteCommand : IRequest<Result<Guid>>
{
}
