using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Order.Commands.OrderCreate;

public class OrderCreateCommand : IRequest<Result<int>>
{
    public int Id { get; set; }  
}