using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Invoice.Commands.InvoiceDelete;

/// <summary>
/// Command for deleting an existing invoice from the system.
/// Implements IRequest to work with MediatR, returning the ID of the deleted invoice wrapped in a Result.
/// </summary>
public class DeleteInvoiceCommand : IRequest<Result<int>>
{
    /// <summary>
    /// The ID of the invoice to delete
    /// </summary>
    public int Id { get; set; }     
}
