using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Commands.SalesCreate;

/// <summary>
/// Handler for processing sales creation commands.
/// Implements IRequestHandler from MediatR to handle SalesCreateCommand requests
/// and return the ID of the newly created sales wrapped in a Result.
/// </summary>
public class SalesCreateHandler : IRequestHandler<SalesCreateCommand, Result<Guid>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<SalesCreateHandler> _logger;

    /// <summary>
    /// Repository for sales data operations
    /// </summary>
    private readonly ISalesRepository _salesRepository;

    /// <summary>
    /// Repository for customer data operations
    /// </summary>
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="salesRepository">Repository for sales data access</param>
    /// <param name="customerRepository">Repository for customer data access</param>
    public SalesCreateHandler(
        IAppLogger<SalesCreateHandler> logger,
        ISalesRepository salesRepository,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesRepository = salesRepository ?? throw new ArgumentNullException(nameof(salesRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    /// <summary>
    /// Handles the sales creation request
    /// </summary>
    /// <param name="request">The sales creation command with sales details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created sales if successful</returns>
    public async Task<Result<Guid>> Handle(SalesCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new sales with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new SalesCreateValidator(_salesRepository, _customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a bad request result with validation error messages
        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(SalesCreateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Auto-generate SalesId if not provided
            var salesId = request.SalesId;
            if (salesId == 0)
            {
                salesId = await _salesRepository.GetNextSalesIdAsync();
            }

            // Manual mapping instead of using AutoMapper
            var salesToCreate = new Domain.Entities.Sales
            {
                SalesId = salesId,
                SalesChannelId = request.SalesChannelId,
                RemoteSalesId = request.RemoteSalesId,
                CustomerId = request.CustomerId,
                Status = request.Status,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = request.PaymentStatus,
                PaymentProvider = request.PaymentProvider,
                PaymentTransactionId = request.PaymentTransactionId,
                CustomerNote = request.CustomerNote,
                InternalNote = request.InternalNote,
                Subtotal = request.Subtotal,
                ShippingCost = request.ShippingCost,
                TotalTax = request.TotalTax,
                Total = request.Total,
                DeliveryAddressFirstName = request.DeliveryAddressFirstName,
                DeliveryAddressLastName = request.DeliveryAddressLastName,
                DeliveryAddressCompanyName = request.DeliveryAddressCompanyName,
                DeliveryAddressPhone = request.DeliveryAddressPhone,
                DeliveryAddressStreet = request.DeliveryAddressStreet,
                DeliveryAddressCity = request.DeliveryAddressCity,
                DeliveryAddressZip = request.DeliveryAddressZip,
                DeliveryAddressCountry = request.DeliveryAddressCountry,
                InvoiceAddressFirstName = request.InvoiceAddressFirstName,
                InvoiceAddressLastName = request.InvoiceAddressLastName,
                InvoiceAddressCompanyName = request.InvoiceAddressCompanyName,
                InvoiceAddressPhone = request.InvoiceAddressPhone,
                InvoiceAddressStreet = request.InvoiceAddressStreet,
                InvoiceAddressCity = request.InvoiceAddressCity,
                InvoiceAddressZip = request.InvoiceAddressZip,
                InvoiceAddressCountry = request.InvoiceAddressCountry,
                DateSalesed = request.DateSalesed.Kind == DateTimeKind.Utc
                    ? request.DateSalesed
                    : request.DateSalesed.ToUniversalTime()
                // SalesItems would need to be mapped separately
            };

            // Add the new sales to the database
            await _salesRepository.CreateAsync(salesToCreate);

            // Set successful result with the new sales ID
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = salesToCreate.Id;

            _logger.LogInformation("Successfully created sales with ID: {Id}", salesToCreate.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during sales creation
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the sales: {ex.Message}");

            _logger.LogError("Error creating sales: {Message}", ex.Message);
        }

        return result;
    }
}