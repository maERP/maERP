using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductCreate;

public class ProductCreateCommand : ProductInputDto, IRequest<Result<int>>
{
}