using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using maERP.Domain.Entities;

namespace maERP.Application.Features.Customer.Commands.CustomerUpdate;

public class CustomerUpdateHandler : IRequestHandler<CustomerUpdateCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating customer with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new CustomerUpdateValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;

            // Check if the validation error is about customer not found
            if (validationResult.Errors.Any(e => e.ErrorMessage.Contains("Customer not found")))
            {
                result.StatusCode = ResultStatusCode.NotFound;
            }
            else
            {
                result.StatusCode = ResultStatusCode.BadRequest;
            }

            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(CustomerUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Get the customer for tracking (required for update)
            var customerToUpdate = await _customerRepository.GetByIdAsync(request.Id);

            if (customerToUpdate == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Customer not found or access denied due to tenant isolation.");

                _logger.LogWarning("Customer with ID {Id} not found or access denied due to tenant isolation", request.Id);
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
                        existingAddress.CountryId = addressDto.CountryId;
                    }
                    else if (addressDto.Id == Guid.Empty)
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
                            CountryId = addressDto.CountryId
                        };

                        await _customerRepository.AddCustomerAddressAsync(newAddress);
                    }
                }
            }

            // Update in database
            await _customerRepository.UpdateAsync(customerToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
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