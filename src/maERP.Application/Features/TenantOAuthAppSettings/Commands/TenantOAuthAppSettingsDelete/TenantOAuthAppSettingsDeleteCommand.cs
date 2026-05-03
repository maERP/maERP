using maERP.Application.Mediator;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantOAuthAppSettings.Commands.TenantOAuthAppSettingsDelete;

public class TenantOAuthAppSettingsDeleteCommand : IRequest<Result<int>>
{
    public SalesChannelType Provider { get; set; }
}
