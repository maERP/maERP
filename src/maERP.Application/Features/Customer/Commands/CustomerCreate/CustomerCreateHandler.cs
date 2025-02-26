using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

public class CustomerCreateHandler : IRequestHandler<CustomerCreateCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CustomerCreateHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerCreateHandler(IMapper mapper,
        IAppLogger<CustomerCreateHandler> logger,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<int>> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new customer with firstname: {Firstname}, lastname: {Lastname}", 
            request.Firstname, request.Lastname);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new CustomerCreateValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}", 
                nameof(CustomerCreateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Map and create entity
            var customerToCreate = _mapper.Map<Domain.Entities.Customer>(request);
            
            // add to database
            await _customerRepository.CreateAsync(customerToCreate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = customerToCreate.Id;
            
            _logger.LogInformation("Successfully created customer with ID: {Id}", customerToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the customer: {ex.Message}");
            
            _logger.LogError("Error creating customer: {Message}", ex.Message);
        }

        return result;
    }
}