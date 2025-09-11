using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Setting.Commands.SettingDelete;

public class SettingDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}