using maERP.Application.Mediator;
using maERP.Domain.Dtos.SystemOAuthSettings;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SystemOAuthSettings.Queries.SystemOAuthSettingsDetail;

public class SystemOAuthSettingsDetailQuery : IRequest<Result<SystemOAuthSettingsDto>>
{
    public SalesChannelType Provider { get; set; }
}
