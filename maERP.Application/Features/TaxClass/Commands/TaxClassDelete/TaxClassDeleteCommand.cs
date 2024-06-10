using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassDelete;

public class TaxClassDeleteCommand : IRequest<int>
{
    public int Id { get; set; }     
}