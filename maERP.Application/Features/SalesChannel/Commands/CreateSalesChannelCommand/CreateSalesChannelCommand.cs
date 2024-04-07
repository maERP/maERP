using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;

public class CreateSalesChannelCommand : IRequest<int>
{
    public double TaxRate { get; set; }  
}