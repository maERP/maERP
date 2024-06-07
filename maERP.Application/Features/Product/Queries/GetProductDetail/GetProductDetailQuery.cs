using MediatR;

namespace maERP.Application.Features.Product.Queries.GetProductDetail;

public class GetProductDetailQuery : IRequest<GetProductDetailResponse>
{
    public int Id { get; set; }
}