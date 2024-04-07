using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannelCommand;

public class UpdateSalesChannelCommand : IRequest<int>
{
    public int Id { get; set; }     
    public double TaxRate { get; set; }
}