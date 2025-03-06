using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductInputCommand : ProductInputDto, IRequest<Result<int>>
{
}