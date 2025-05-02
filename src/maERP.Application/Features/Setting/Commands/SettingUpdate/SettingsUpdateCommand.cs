using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Setting.Commands.SettingUpdate;

public class SettingUpdateCommand : SettingInputDto, IRequest<Result<int>>
{
}