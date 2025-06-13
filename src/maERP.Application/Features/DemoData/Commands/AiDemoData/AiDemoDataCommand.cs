using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.DemoData.Commands.AiDemoData;

public class AiDemoDataCommand : IRequest<Result<string>>
{
}