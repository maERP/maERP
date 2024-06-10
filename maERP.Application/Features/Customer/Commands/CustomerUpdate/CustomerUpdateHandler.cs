using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateHandler : IRequestHandler<CustomerUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CustomerUpdateHandler> _logger;
    private readonly ICustomerRepository _customerRepository;


    public CustomerUpdateHandler(IMapper mapper,
        IAppLogger<CustomerUpdateHandler> logger,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
    {
        var validator = new CustomerUpdateValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(CustomerUpdateCommand), request.Id);
            throw new ValidationException("Invalid Customer", validationResult);
        }

        var customerToUpdate = _mapper.Map<Domain.Models.Customer>(request);

        await _customerRepository.UpdateAsync(customerToUpdate);

        return customerToUpdate.Id;
    }
}