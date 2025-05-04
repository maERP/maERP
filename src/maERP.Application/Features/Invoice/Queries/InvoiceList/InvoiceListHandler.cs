using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Invoice.Queries.InvoiceList;

/// <summary>
/// Handler for processing invoice list queries.
/// Implements IRequestHandler from MediatR to handle InvoiceListQuery requests
/// and return a paginated list of invoice DTOs.
/// </summary>
public class InvoiceListHandler : IRequestHandler<InvoiceListQuery, PaginatedResult<InvoiceListDto>>
{
    private readonly IAppLogger<InvoiceListHandler> _logger;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="invoiceRepository">Repository for invoice data access</param>
    /// <param name="customerRepository">Repository for customer data access</param>
    public InvoiceListHandler(
        IAppLogger<InvoiceListHandler> logger, 
        IInvoiceRepository invoiceRepository,
        ICustomerRepository customerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    /// <summary>
    /// Handles the invoice list query request
    /// </summary>
    /// <param name="request">The query with pagination and filtering parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated result containing invoice list DTOs</returns>
    public async Task<PaginatedResult<InvoiceListDto>> Handle(InvoiceListQuery request, CancellationToken cancellationToken)
    {
        var invoiceFilterSpec = new InvoiceFilterSpecification(request.SearchString);
        
        _logger.LogInformation("Handle InvoiceListQuery: {0}", request);

        // Get all customers for joining customer names
        var customers = await _customerRepository.GetAllAsync();
        
        // If no sorting parameters provided
        if (request.OrderBy.Any() != true)
        {
            return await _invoiceRepository.Entities
               .Specify(invoiceFilterSpec)
               .Select(i => new InvoiceListDto
               {
                   Id = i.Id,
                   InvoiceNumber = i.InvoiceNumber,
                   InvoiceDate = i.InvoiceDate,
                   CustomerId = i.CustomerId,
                   OrderId = i.OrderId,
                   OrderNumber = i.OrderId.HasValue ? i.OrderId.Value.ToString() : string.Empty,
                   Total = i.Total,
                   PaymentStatus = i.PaymentStatus,
                   InvoiceStatus = i.InvoiceStatus,
                   PaymentMethod = i.PaymentMethod
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        // If sorting parameters are provided
        var ordering = string.Join(",", request.OrderBy);

        return await _invoiceRepository.Entities
            .Specify(invoiceFilterSpec)
            .OrderBy(ordering)
            .Select(i => new InvoiceListDto
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                InvoiceDate = i.InvoiceDate,
                CustomerId = i.CustomerId,
                OrderId = i.OrderId,
                OrderNumber = i.OrderId.HasValue ? i.OrderId.Value.ToString() : string.Empty,
                Total = i.Total,
                PaymentStatus = i.PaymentStatus,
                InvoiceStatus = i.InvoiceStatus,
                PaymentMethod = i.PaymentMethod
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
