using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Commands.SalesUpdate;

public class SalesUpdateHandler : IRequestHandler<SalesUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<SalesUpdateHandler> _logger;
    private readonly ISalesRepository _salesRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPdfService _pdfService;


    public SalesUpdateHandler(
        IAppLogger<SalesUpdateHandler> logger,
        ISalesRepository salesRepository,
        ICustomerRepository customerRepository,
        IInvoiceRepository invoiceRepository,
        IPdfService pdfService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesRepository = salesRepository ?? throw new ArgumentNullException(nameof(salesRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));
    }

    public async Task<Result<Guid>> Handle(SalesUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating sales with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new SalesUpdateValidator(_salesRepository, _customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(SalesUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // First check if sales exists globally
            var existsGlobally = await _salesRepository.ExistsGloballyAsync(request.Id);
            if (!existsGlobally)
            {
                // Sales doesn't exist at all
                _logger.LogWarning("Sales not found: {SalesId}", request.Id);
                throw new NotFoundException("Sales", request.Id);
            }

            // Check tenant isolation - sales exists globally but might belong to different tenant
            var existsForCurrentTenant = await _salesRepository.ExistsAsync(request.Id);
            if (!existsForCurrentTenant)
            {
                // Sales exists globally but not for current tenant - cross-tenant access attempt
                _logger.LogWarning("Cross-tenant access attempt for sales {SalesId}", request.Id);
                throw new NotFoundException("Sales", request.Id);
            }

            // Validate customer belongs to current tenant
            var customer = await _customerRepository.GetByCustomerIdAsync(request.CustomerId);
            if (customer == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.Add("Der angegebene Kunde existiert nicht oder gehört nicht zu Ihrem Tenant.");
                _logger.LogWarning("Cross-tenant customer access attempt for customer {CustomerId}", request.CustomerId);
                return result;
            }

            // Manuelles Mapping statt AutoMapper
            var salesToUpdate = new Domain.Entities.Sales
            {
                Id = request.Id,
                SalesId = request.SalesId,
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
                // SalesItems müssten separat gemappt werden
            };

            // Update in database
            await _salesRepository.UpdateAsync(salesToUpdate);

            // Prüfen, ob eine Rechnung erstellt werden kann
            bool canCreateInvoice = await _salesRepository.CanCreateInvoice(salesToUpdate.Id);

            if (canCreateInvoice)
            {
                try
                {
                    // Verkauf mit Details laden
                    var salesWithDetails = await _salesRepository.GetWithDetailsAsync(salesToUpdate.Id);

                    if (salesWithDetails != null)
                    {
                        // Rechnung erstellen mit der ausgelagerten Methode
                        await _invoiceRepository.CreateInvoiceFromSalesAsync(salesWithDetails);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error creating invoice for sales ID {Id}: {Message}", salesToUpdate.Id, ex.Message);
                }
            }

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = salesToUpdate.Id;

            _logger.LogInformation("Successfully updated sales with ID: {Id}", salesToUpdate.Id);
        }
        catch (NotFoundException)
        {
            // Let NotFoundException bubble up to middleware for proper 404 handling
            throw;
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the sales: {ex.Message}");

            _logger.LogError("Error updating sales: {Message}", ex.Message);
        }

        return result;
    }
}
