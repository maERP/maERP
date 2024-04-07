using maERP.Application.Dtos.Product;
using MediatR;

namespace maERP.Application.Features.Product.Queries.GetProductsQuery;

public record GetProductsQuery : IRequest<List<ProductListDto>>;