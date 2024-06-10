using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
}