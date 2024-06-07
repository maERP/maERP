using MediatR;

namespace maERP.Application.Features.Product.Queries.GetProducts;

public record GetProductsQuery : IRequest<List<GetProductsResponse>>;