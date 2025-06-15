using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductDelete;

public class ProductDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
}