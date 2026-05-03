using maERP.Application.Mediator;
using maERP.Domain.Dtos.TenantOAuthAppSettings;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantOAuthAppSettings.Queries.TenantOAuthAppSettingsDetail;

public class TenantOAuthAppSettingsDetailQuery : IRequest<Result<TenantOAuthAppSettingsDetailDto>>
{
    public SalesChannelType Provider { get; set; }
}
