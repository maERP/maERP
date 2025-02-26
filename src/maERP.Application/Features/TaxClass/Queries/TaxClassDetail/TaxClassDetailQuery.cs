using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassDetail;

public class TaxClassDetailQuery : IRequest<Result<TaxClassDetailDto>>
{
    public int Id { get; set; }
}