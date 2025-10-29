namespace maERP.Client.Features.Invoices.Models;

public partial record InvoiceListModel
{
    private readonly IInvoicesApiClient _invoicesApiClient;

    public InvoiceListModel(IInvoicesApiClient invoicesApiClient)
    {
        _invoicesApiClient = invoicesApiClient;
    }
}
