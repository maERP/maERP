using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.DemoData.Commands.ClearAllData;

public class ClearAllDataCommand : IRequest<Result<string>>
{
}