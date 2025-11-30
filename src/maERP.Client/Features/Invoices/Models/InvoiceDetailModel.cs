using maERP.Client.Features.Invoices.Services;
using maERP.Domain.Dtos.Invoice;

namespace maERP.Client.Features.Invoices.Models;

/// <summary>
/// Model for invoice detail page using MVUX pattern.
/// This is a read-only view of invoice details.
/// </summary>
public partial record InvoiceDetailModel
{
    private readonly IInvoiceService _invoiceService;
    private readonly INavigator _navigator;
    private readonly Guid _invoiceId;

    public InvoiceDetailModel(
        IInvoiceService invoiceService,
        INavigator navigator,
        InvoiceDetailData data)
    {
        _invoiceService = invoiceService;
        _navigator = navigator;
        _invoiceId = data.invoiceId;
    }

    /// <summary>
    /// Feed that loads the invoice details.
    /// </summary>
    public IFeed<InvoiceDetailDto> Invoice => Feed.Async(async ct =>
    {
        var invoice = await _invoiceService.GetInvoiceAsync(_invoiceId, ct);
        return invoice ?? throw new InvalidOperationException($"Invoice {_invoiceId} not found");
    });

    /// <summary>
    /// Navigate back to invoice list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }
}
