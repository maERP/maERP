using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.CreateTaxClassCommand;

public class CreateTaxClassCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;     
}