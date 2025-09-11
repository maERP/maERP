using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateCommand : ProductInputDto, IRequest<Result<Guid>>
{
}