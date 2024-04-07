using MediatR;

namespace maERP.Application.Features.Product.Commands.DeleteProductCommand;

public class DeleteProductCommand : IRequest<int>
{
    public int Id { get; set; }     
}