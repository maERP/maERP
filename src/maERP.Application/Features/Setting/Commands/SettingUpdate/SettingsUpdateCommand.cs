using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Setting.Commands.SettingUpdate;

public class SettingUpdateCommand : SettingInputDto, IRequest<Result<Guid>>
{
}