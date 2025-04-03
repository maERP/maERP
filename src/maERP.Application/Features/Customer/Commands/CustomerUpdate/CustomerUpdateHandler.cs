using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;
using maERP.Domain.Entities;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateHandler : IRequestHandler<CustomerUpdateCommand, Result<int>>
{
    private readonly IAppLogger<CustomerUpdateHandler> _logger;
    private readonly ICustomerRepository _customerRepository;


    public CustomerUpdateHandler(
        IAppLogger<CustomerUpdateHandler> logger,
        ICustomerRepository customerRepository)
    {
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
            // Manual mapping instead of AutoMapper
            var customerToUpdate = await _customerRepository.GetByIdAsync(request.Id);
            
            if (customerToUpdate == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Customer with ID {request.Id} not found");
                return result;
            }
            
            // Manual assignment of properties
            customerToUpdate.Firstname = request.Firstname;
            customerToUpdate.Lastname = request.Lastname;
            customerToUpdate.CompanyName = request.CompanyName;
            customerToUpdate.Email = request.Email;
            customerToUpdate.Phone = request.Phone;
            customerToUpdate.Website = request.Website;
            customerToUpdate.VatNumber = request.VatNumber;
            customerToUpdate.Note = request.Note;
            customerToUpdate.CustomerStatus = request.CustomerStatus;
            customerToUpdate.DateEnrollment = request.DateEnrollment;
            
            // Update CustomerAddresses if available
            if (request.CustomerAddresses.Any())
            {
                // Load existing addresses
                var existingAddresses = await _customerRepository.GetCustomerAddressByCustomerIdAsync(request.Id);
                
                foreach (var addressDto in request.CustomerAddresses)
                {
                    // Search for existing address
                    var existingAddress = existingAddresses.FirstOrDefault(a => a.Id == addressDto.Id);
                    
                    if (existingAddress != null)
                    {
                        // Update existing address
                        existingAddress.Firstname = addressDto.Firstname;
                        existingAddress.Lastname = addressDto.Lastname;
                        existingAddress.CompanyName = addressDto.CompanyName;
                        existingAddress.Street = addressDto.Street;
                        existingAddress.HouseNr = addressDto.HouseNr;
                        existingAddress.Zip = addressDto.Zip;
                        existingAddress.City = addressDto.City;
                        existingAddress.DefaultDeliveryAddress = addressDto.DefaultDeliveryAddress;
                        existingAddress.DefaultInvoiceAddress = addressDto.DefaultInvoiceAddress;
                    }
                    else if (addressDto.Id == 0)
                    {
                        // Add new address
                        var newAddress = new CustomerAddress
                        {
                            CustomerId = request.Id,
                            Firstname = addressDto.Firstname,
                            Lastname = addressDto.Lastname,
                            CompanyName = addressDto.CompanyName,
                            Street = addressDto.Street,
                            HouseNr = addressDto.HouseNr,
                            Zip = addressDto.Zip,
                            City = addressDto.City,
                            DefaultDeliveryAddress = addressDto.DefaultDeliveryAddress,
                            DefaultInvoiceAddress = addressDto.DefaultInvoiceAddress,
                            // Using a fixed default value for CountryId as it's missing in CustomerAddressListDto
                            CountryId = 1 // Default country (needs to be adjusted)
                        };
                        
                        await _customerRepository.AddCustomerAddressAsync(newAddress);
                    }
                }
            }
            
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