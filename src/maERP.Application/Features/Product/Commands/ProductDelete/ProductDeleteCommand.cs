using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductDelete;

public class ProductDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}