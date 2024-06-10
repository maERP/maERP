using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

public class CustomerCreateHandler : IRequestHandler<CustomerCreateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CustomerCreateHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerCreateHandler(IMapper mapper,
        IAppLogger<CustomerCreateHandler> logger,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CustomerCreateValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0}", nameof(CustomerCreateCommand));
            throw new ValidationException("Invalid Customer", validationResult);
        }

        // convert to domain entity object
        var customerToCreate = _mapper.Map<Domain.Models.Customer>(request);

        // add to database
        await _customerRepository.CreateAsync(customerToCreate);

        // return record id
        return customerToCreate.Id;
    }
}