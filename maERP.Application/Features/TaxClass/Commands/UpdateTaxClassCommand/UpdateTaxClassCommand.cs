using MediatR;

namespace maERP.TaxClass.Features.TaxClass.Commands.UpdateTaxClassCommand;

public class UpdateTaxClassCommand : IRequest<int>
{
    public int Id { get; set; }     
    public string Name { get; set; } = string.Empty;
}