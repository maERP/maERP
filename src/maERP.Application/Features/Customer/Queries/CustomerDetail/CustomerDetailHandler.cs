using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Customer.Queries.CustomerDetail;

public class CustomerDetailHandler : IRequestHandler<CustomerDetailQuery, Result<CustomerDetailDto>>
{
    private readonly IAppLogger<CustomerDetailHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerDetailHandler(
        IAppLogger<CustomerDetailHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }
    
    public async Task<Result<CustomerDetailDto>> Handle(CustomerDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving customer details for ID: {Id}", request.Id);
        
        var result = new Result<CustomerDetailDto>();
        
        try
        {
            var customer = await _customerRepository.GetCustomerWithDetails(request.Id);

            if (customer == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Customer with ID {request.Id} not found");
                
                _logger.LogWarning("Customer with ID {Id} not found", request.Id);
                return result;
            }

            // Manuelles Mapping statt AutoMapper
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

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("Customer with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the customer: {ex.Message}");
            
            _logger.LogError("Error retrieving customer: {Message}", ex.Message);
        }
        
        return result;
    }
}