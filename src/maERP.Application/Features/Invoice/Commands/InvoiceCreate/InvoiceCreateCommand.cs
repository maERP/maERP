using maERP.Application.Features.Invoice.Commands.InvoiceUpdate;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Invoice.Commands.InvoiceCreate;

/// <summary>
/// Command for creating a new invoice in the system.
/// Inherits from CreateInvoiceDto to get all invoice properties and implements IRequest
/// to work with MediatR, returning the ID of the newly created invoice wrapped in a Result.
/// </summary>
public class InvoiceCreateCommand : InvoiceInputDto, IRequest<Result<int>>
{
}
