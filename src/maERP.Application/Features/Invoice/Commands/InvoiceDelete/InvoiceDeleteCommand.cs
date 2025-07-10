using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Invoice.Commands.InvoiceDelete;

/// <summary>
/// Command for deleting an existing invoice from the system.
/// Implements IRequest to work with MediatR, returning the ID of the deleted invoice wrapped in a Result.
/// </summary>
public class InvoiceDeleteCommand : IRequest<Result<int>>
{
    /// <summary>
    /// The ID of the invoice to delete
    /// </summary>
    public int Id { get; set; }
}
