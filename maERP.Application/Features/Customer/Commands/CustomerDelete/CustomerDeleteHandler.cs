using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerDelete;

public class CustomerDeleteHandler : IRequestHandler<CustomerDeleteCommand, int>
{
    private readonly IAppLogger<CustomerDeleteHandler> _logger;
    private readonly ICustomerRepository _customerRepository;


    public CustomerDeleteHandler(
        IAppLogger<CustomerDeleteHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new CustomerDeleteValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(CustomerDeleteCommand), request.Id);
            throw new ValidationException("Invalid Customer", validationResult);
        }

        var customerToDelete = new Domain.Entities.Customer()
        {
            Id = request.Id
        };

        await _customerRepository.DeleteAsync(customerToDelete);

        return customerToDelete.Id;
    }
}