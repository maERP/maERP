using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Sales;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Queries.SalesDetail;

/// <summary>
/// Handler for processing sales detail queries.
/// Implements IRequestHandler from MediatR to handle SalesDetailQuery requests
/// and return detailed sales information wrapped in a Result.
/// </summary>
public class SalesDetailHandler : IRequestHandler<SalesDetailQuery, Result<SalesDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<SalesDetailHandler> _logger;

    /// <summary>
    /// Repository for sales data operations
    /// </summary>
    private readonly ISalesRepository _salesRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="salesRepository">Repository for sales data access</param>
    public SalesDetailHandler(
        IAppLogger<SalesDetailHandler> logger,
        ISalesRepository salesRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesRepository = salesRepository ?? throw new ArgumentNullException(nameof(salesRepository));
    }

    /// <summary>
    /// Handles the sales detail query request
    /// </summary>
    /// <param name="request">The query containing the sales ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed sales information if successful</returns>
    public async Task<Result<SalesDetailDto>> Handle(SalesDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving sales details for ID: {Id}", request.Id);

        var result = new Result<SalesDetailDto>();

        try
        {
            // Retrieve sales with all related details from the repository
            var sales = await _salesRepository.GetWithDetailsAsync(request.Id);

            // If sales not found, return a not found result
            if (sales == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Sales with ID {request.Id} not found");

                _logger.LogWarning("Sales with ID {Id} not found", request.Id);
                return result;
            }

            // Bestellhistorie abrufen
            var salesHistory = await _salesRepository.GetSalesHistoryAsync(request.Id);

            // Manual mapping from entity to DTO
            var data = new SalesDetailDto
            {
                Id = sales.Id,
                SalesId = sales.SalesId,
                SalesChannelId = sales.SalesChannelId,
                RemoteSalesId = sales.RemoteSalesId,
                CustomerId = sales.Customer.CustomerId,
                Status = sales.Status,
                SalesItems = sales.SalesItems.ToList(),
                SalesHistory = MapSalesHistoryToDto(salesHistory),
                PaymentMethod = sales.PaymentMethod,
                PaymentStatus = sales.PaymentStatus,
                PaymentProvider = sales.PaymentProvider,
                PaymentTransactionId = sales.PaymentTransactionId,
                // Shipping fields not available in the entity, using empty values
                ShippingMethod = string.Empty,
                ShippingStatus = string.Empty,
                ShippingProvider = string.Empty,
                ShippingTrackingId = string.Empty,
                Subtotal = sales.Subtotal,
                ShippingCost = sales.ShippingCost,
                TotalTax = sales.TotalTax,
                Total = sales.Total,
                Note = sales.CustomerNote,
                // Delivery address details
                DeliveryAddressFirstName = sales.DeliveryAddressFirstName,
                DeliveryAddressLastName = sales.DeliveryAddressLastName,
                DeliveryAddressCompanyName = sales.DeliveryAddressCompanyName,
                DeliveryAddressPhone = sales.DeliveryAddressPhone,
                DeliveryAddressStreet = sales.DeliveryAddressStreet,
                DeliveryAddressCity = sales.DeliveryAddressCity,
                DeliveryAddressZip = sales.DeliveryAddressZip,
                DeliveryAddressCountry = sales.DeliveryAddressCountry,
                // Invoice address details
                InvoiceAddressFirstName = sales.InvoiceAddressFirstName,
                InvoiceAddressLastName = sales.InvoiceAddressLastName,
                InvoiceAddressCompanyName = sales.InvoiceAddressCompanyName,
                InvoiceAddressPhone = sales.InvoiceAddressPhone,
                InvoiceAddressStreet = sales.InvoiceAddressStreet,
                InvoiceAddressCity = sales.InvoiceAddressCity,
                InvoiceAddressZip = sales.InvoiceAddressZip,
                InvoiceAddressCountry = sales.InvoiceAddressCountry,
                DateSalesed = sales.DateSalesed
            };

            // Set successful result with the sales details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data; 

            _logger.LogInformation("Sales with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during sales retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the sales: {ex.Message}");

            _logger.LogError("Error retrieving sales: {Message}", ex.Message);
        }

        return result;
    }

    /// <summary>
    /// Konvertiert SalesHistory-Entities in SalesHistoryDto-Objekte
    /// </summary>
    /// <param name="salesHistories">Liste von SalesHistory-Entities</param>
    /// <returns>Liste von SalesHistoryDto-Objekten</returns>
    private List<SalesHistoryDto> MapSalesHistoryToDto(List<Domain.Entities.SalesHistory> salesHistories)
    {
        return salesHistories.Select(history => new SalesHistoryDto
        {
            Id = history.Id,
            UserId = history.UserId,
            SalesId = history.SalesId,
            SalesStatusOld = history.SalesStatusOld,
            SalesStatusNew = history.SalesStatusNew,
            PaymentStatusOld = history.PaymentStatusOld,
            PaymentStatusNew = history.PaymentStatusNew,
            ShippingStatusOld = history.ShippingStatusOld,
            ShippingStatusNew = history.ShippingStatusNew,
            Description = history.Description,
            IsSystemGenerated = history.IsSystemGenerated
        }).ToList();
    }
}