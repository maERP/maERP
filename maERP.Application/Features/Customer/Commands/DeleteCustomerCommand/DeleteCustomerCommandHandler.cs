using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.DeleteCustomerCommand;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;


    public DeleteCustomerCommandHandler(IMapper mapper,
        IAppLogger<DeleteCustomerCommandHandler> logger,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new DeleteCustomerCommandValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(CreateCustomerCommand), request.Id);
            throw new Exceptions.ValidationException("Invalid Customer", validationResult);
        }

        // convert to domain entity object
        var customerToDelete = _mapper.Map<Domain.Customer>(request);

        // add to database
        await _customerRepository.CreateAsync(customerToDelete);

        // return record id
        return customerToDelete.Id;
    }
}
