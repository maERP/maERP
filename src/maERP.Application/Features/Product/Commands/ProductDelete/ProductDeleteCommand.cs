using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductDelete;

public class ProductDeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}