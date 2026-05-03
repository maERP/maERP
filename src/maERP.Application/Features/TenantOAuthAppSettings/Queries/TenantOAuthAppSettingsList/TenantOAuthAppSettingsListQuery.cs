using maERP.Application.Mediator;
using maERP.Domain.Dtos.TenantOAuthAppSettings;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.TenantOAuthAppSettings.Queries.TenantOAuthAppSettingsList;

public class TenantOAuthAppSettingsListQuery : IRequest<Result<List<TenantOAuthAppSettingsListDto>>>
{
}
