using maERP.Domain.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassDetail;

public class TaxClassDetailQuery : IRequest<TaxClassDetailDto>
{
    public int Id { get; set; }
}