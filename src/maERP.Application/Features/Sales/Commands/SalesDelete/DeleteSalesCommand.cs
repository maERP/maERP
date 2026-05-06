using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Commands.SalesDelete;

public class DeleteSalesCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}