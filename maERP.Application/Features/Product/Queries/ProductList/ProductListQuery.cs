using MediatR;

namespace maERP.Application.Features.Product.Queries.ProductList;

public record ProductListQuery : IRequest<List<ProductListResponse>>;