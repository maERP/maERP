using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Customer.Commands.CustomerCreate;

/// <summary>
/// Handler for processing customer creation commands.
/// Implements IRequestHandler from custom mediator to handle CustomerCreateCommand requests
/// and return the ID of the newly created customer wrapped in a Result.
/// </summary>
public class CustomerCreateHandler : IRequestHandler<CustomerCreateCommand, Result<Guid>>
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
    public async Task<Result<Guid>> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new customer with firstname: {Firstname}, lastname: {Lastname}",
            request.Firstname, request.Lastname);

        // Validate incoming data
        var validator = new CustomerCreateValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            var validationErrors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(CustomerCreateCommand), validationErrors);

            return Result<Guid>.Fail(ResultStatusCode.BadRequest, validationErrors);
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

            _logger.LogInformation("Successfully created customer with ID: {Id}", customerToCreate.Id);
            
            var result = Result<Guid>.Success(customerToCreate.Id);
            result.StatusCode = ResultStatusCode.Created;
            return result;
        }
        catch (Exception ex)
        {
            // Handle any exceptions during customer creation
            _logger.LogError("Error creating customer: {Message}", ex.Message);
            
            return Result<Guid>.Fail(ResultStatusCode.InternalServerError,
                $"An error occurred while creating the customer: {ex.Message}");
        }
    }
}