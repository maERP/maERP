using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Setting.Commands.SettingDelete;

public class SettingDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }     
}