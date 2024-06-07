using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.DeleteCustomer;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, int>
{
    private readonly IAppLogger<DeleteCustomerHandler> _logger;
    private readonly ICustomerRepository _customerRepository;


    public DeleteCustomerHandler(
        IAppLogger<DeleteCustomerHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCustomerValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(DeleteCustomerCommand), request.Id);
            throw new ValidationException("Invalid Customer", validationResult);
        }

        var customerToDelete = new Domain.Models.Customer()
        {
            Id = request.Id
        };

        await _customerRepository.DeleteAsync(customerToDelete);

        return customerToDelete.Id;
    }
}