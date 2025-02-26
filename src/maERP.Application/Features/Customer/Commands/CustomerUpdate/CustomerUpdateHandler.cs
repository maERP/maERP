using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateHandler : IRequestHandler<CustomerUpdateCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CustomerUpdateHandler> _logger;
    private readonly ICustomerRepository _customerRepository;


    public CustomerUpdateHandler(IMapper mapper,
        IAppLogger<CustomerUpdateHandler> logger,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<int>> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating customer with ID: {Id}", request.Id);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new CustomerUpdateValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in update request for {0}: {1}", 
                nameof(CustomerUpdateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Map to domain entity
            var customerToUpdate = _mapper.Map<Domain.Entities.Customer>(request);
            
            // Update in database
            await _customerRepository.UpdateAsync(customerToUpdate);
            
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = customerToUpdate.Id;
            
            _logger.LogInformation("Successfully updated customer with ID: {Id}", customerToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the customer: {ex.Message}");
            
            _logger.LogError("Error updating customer: {Message}", ex.Message);
        }

        return result;
    }
}