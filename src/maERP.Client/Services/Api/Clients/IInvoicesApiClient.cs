using maERP.Application.Features.Invoice.Commands.InvoiceCreate;
using maERP.Application.Features.Invoice.Commands.InvoiceUpdate;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for invoice operations
/// </summary>
public interface IInvoicesApiClient
{
    /// <summary>
    /// Get paginated list of invoices
    /// </summary>
    Task<PaginatedResult<InvoiceListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get invoice details by ID
    /// </summary>
    Task<InvoiceDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get invoice as PDF
    /// </summary>
    Task<HttpResponseMessage> GetPdfAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new invoice
    /// </summary>
    Task<HttpResponseMessage> CreateAsync(InvoiceCreateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing invoice
    /// </summary>
    Task<HttpResponseMessage> UpdateAsync(Guid id, InvoiceUpdateCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an invoice
    /// </summary>
    Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
