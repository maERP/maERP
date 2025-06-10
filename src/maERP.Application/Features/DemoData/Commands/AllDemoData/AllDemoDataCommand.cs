using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.DemoData.Commands.AllDemoData;

public class AllDemoDataCommand : IRequest<Result<string>>
{
}