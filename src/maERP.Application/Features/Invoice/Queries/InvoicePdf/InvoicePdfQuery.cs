using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.Invoice.Queries.InvoicePdf;

public record InvoicePdfQuery : IRequest<Result<byte[]>>
{
    public int Id { get; set; }
}