using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Customer.Commands.CustomerDelete;

public class CustomerDeleteHandler : IRequestHandler<CustomerDeleteCommand, Result<int>>
{
    private readonly IAppLogger<CustomerDeleteHandler> _logger;
    private readonly ICustomerRepository _customerRepository;


    public CustomerDeleteHandler(
        IAppLogger<CustomerDeleteHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<int>> Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting customer with ID: {Id}", request.Id);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new CustomerDeleteValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(CustomerDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Get entity from database first
            var customerToDelete = await _customerRepository.GetByIdAsync(request.Id);

            if (customerToDelete == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Customer not found");

                _logger.LogWarning("Customer with ID: {Id} not found for deletion", request.Id);
                return result;
            }

            // Delete from database
            await _customerRepository.DeleteAsync(customerToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
            result.Data = customerToDelete.Id;

            _logger.LogInformation("Successfully deleted customer with ID: {Id}", customerToDelete.Id);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
        {
            // Handle concurrent deletion - customer was already deleted by another request
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add("Customer not found");

            _logger.LogWarning("Customer with ID: {Id} was deleted by another request: {Message}", request.Id, ex.Message);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the customer: {ex.Message}");

            _logger.LogError("Error deleting customer: {Message}", ex.Message);
        }

        return result;
    }
}