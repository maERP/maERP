using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Invoice.Queries.InvoiceDetail;

/// <summary>
/// Query for retrieving detailed information about a specific invoice.
/// Implements IRequest to work with MediatR, returning invoice details wrapped in a Result.
/// </summary>
public class InvoiceDetailQuery : IRequest<Result<InvoiceDetailDto>>
{
    /// <summary>
    /// The unique identifier of the invoice to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
