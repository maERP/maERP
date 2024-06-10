using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;

public class TaxClassUpdateCommand : IRequest<int>
{
    public int Id { get; set; }     
    public double TaxRate { get; set; }
}