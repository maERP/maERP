using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

/// <summary>
/// Handler for processing customer detail queries.
/// Implements IRequestHandler from MediatR to handle CustomerDetailQuery requests
/// and return detailed customer information wrapped in a Result.
/// </summary>
public class CustomerDetailHandler : IRequestHandler<CustomerDetailQuery, Result<CustomerDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<CustomerDetailHandler> _logger;

    /// <summary>
    /// Repository for customer data operations
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="customerRepository">Repository for customer data access</param>
    public CustomerDetailHandler(
        IAppLogger<CustomerDetailHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    /// <summary>
    /// Handles the customer detail query request
    /// </summary>
    /// <param name="request">The query containing the customer ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed customer information if successful</returns>
    public async Task<Result<CustomerDetailDto>> Handle(CustomerDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving customer details for ID: {Id}", request.Id);

        var result = new Result<CustomerDetailDto>();

        try
        {
            // Retrieve customer with all related details from the repository
            var customer = await _customerRepository.GetCustomerWithDetails(request.Id);

            // If customer not found, return a not found result
            if (customer == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Customer with ID {request.Id} not found");

                _logger.LogWarning("Customer with ID {Id} not found", request.Id);
                return result;
            }

            // Manual mapping instead of using AutoMapper
            var data = new CustomerDetailDto
            {
                Id = customer.Id,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                CompanyName = customer.CompanyName,
                Email = customer.Email,
                Phone = customer.Phone,
                Website = customer.Website,
                VatNumber = customer.VatNumber,
                Note = customer.Note,
                CustomerStatus = customer.CustomerStatus,
                DateEnrollment = customer.DateEnrollment,
                // Map customer addresses if they exist, otherwise return an empty list
                CustomerAddresses = customer.CustomerAddresses?.Select(ca => new CustomerAddressListDto
                {
                    Id = ca.Id,
                    Firstname = ca.Firstname,
                    Lastname = ca.Lastname,
                    CompanyName = ca.CompanyName,
                    Street = ca.Street,
                    HouseNr = ca.HouseNr,
                    Zip = ca.Zip,
                    City = ca.City,
                    DefaultDeliveryAddress = ca.DefaultDeliveryAddress,
                    DefaultInvoiceAddress = ca.DefaultInvoiceAddress
                }).ToList() ?? new List<CustomerAddressListDto>()
            };

            // Set successful result with the customer details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("Customer with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during customer retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the customer: {ex.Message}");

            _logger.LogError("Error retrieving customer: {Message}", ex.Message);
        }

        return result;
    }
}