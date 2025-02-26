using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassDelete;

public class TaxClassDeleteCommand : IRequest<Result<int>>
{
    public int Id { get; set; }     
}