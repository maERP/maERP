using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassCreate;

public class TaxClassCreateCommand : IRequest<Result<int>>
{
    public double TaxRate { get; set; }  
}