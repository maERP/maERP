using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateCommand : ProductUpdateDto, IRequest<Result<int>>
{
}