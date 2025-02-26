using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;

public class TaxClassUpdateCommand : IRequest<Result<int>>
{
    public int Id { get; set; }     
    public double TaxRate { get; set; }
}