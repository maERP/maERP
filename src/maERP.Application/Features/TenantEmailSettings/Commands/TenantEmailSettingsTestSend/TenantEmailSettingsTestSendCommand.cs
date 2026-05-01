using maERP.Application.Mediator;
using maERP.Domain.Dtos.TenantEmailSettings;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantEmailSettings.Commands.TenantEmailSettingsTestSend;

/// <summary>
/// Sends a verification email using the effective configuration (tenant override merged
/// with server defaults). Useful for verifying that SMTP/Microsoft 365 credentials are valid.
/// </summary>
public class TenantEmailSettingsTestSendCommand : TenantEmailSettingsTestSendDto, IRequest<Result<bool>>
{
}
