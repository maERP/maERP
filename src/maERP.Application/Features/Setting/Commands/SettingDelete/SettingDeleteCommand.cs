using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Setting.Commands.SettingDelete;

public class SettingDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}