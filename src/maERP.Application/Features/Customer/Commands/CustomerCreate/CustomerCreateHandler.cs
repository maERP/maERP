using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

/// <summary>
/// Handler for processing customer creation commands.
/// Implements IRequestHandler from MediatR to handle CustomerCreateCommand requests
/// and return the ID of the newly created customer wrapped in a Result.
/// </summary>
public class CustomerCreateHandler : IRequestHandler<CustomerCreateCommand, Result<int>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<CustomerCreateHandler> _logger;
    
    /// <summary>
    /// Repository for customer data operations
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="customerRepository">Repository for customer data access</param>
    public CustomerCreateHandler(
        IAppLogger<CustomerCreateHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    /// <summary>
    /// Handles the customer creation request
    /// </summary>
    /// <param name="request">The customer creation command with customer details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created customer if successful</returns>
    public async Task<Result<int>> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new customer with firstname: {Firstname}, lastname: {Lastname}", 
            request.Firstname, request.Lastname);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new CustomerCreateValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
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
            // Manual mapping instead of using AutoMapper
            var customerToCreate = new Domain.Entities.Customer
            {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                CompanyName = request.CompanyName,
                Email = request.Email,
                Phone = request.Phone,
                Website = request.Website,
                VatNumber = request.VatNumber,
                Note = request.Note,
                CustomerStatus = request.CustomerStatus,
                DateEnrollment = request.DateEnrollment
                // CustomerAddresses would require additional mapping logic
            };
            
            // Add the new customer to the database
            await _customerRepository.CreateAsync(customerToCreate);

            // Set successful result with the new customer ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = customerToCreate.Id;
            
            _logger.LogInformation("Successfully created customer with ID: {Id}", customerToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during customer creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the customer: {ex.Message}");
            
            _logger.LogError("Error creating customer: {Message}", ex.Message);
        }

        return result;
    }
}