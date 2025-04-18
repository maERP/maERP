using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Invoice.Commands.InvoiceUpdate;

/// <summary>
/// Command for updating an existing invoice in the system.
/// Inherits from UpdateInvoiceDto to get all invoice properties and implements IRequest
/// to work with MediatR, returning the ID of the updated invoice wrapped in a Result.
/// </summary>
public class InvoiceUpdateCommand : InvoiceInputDto, IRequest<Result<int>>
{
}
