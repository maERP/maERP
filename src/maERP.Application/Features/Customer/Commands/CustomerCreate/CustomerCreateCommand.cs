using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

/// <summary>
/// Command for creating a new customer in the system.
/// Inherits from CustomerInputDto to get all customer properties and implements IRequest
/// to work with MediatR, returning the ID of the newly created customer wrapped in a Result.
/// </summary>
public class CustomerCreateCommand : CustomerInputDto, IRequest<Result<int>>
{
}