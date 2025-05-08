using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace maERP.SharedUI.Pages.Invoices;

public partial class InvoicesDetail : ComponentBase
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int InvoiceId { get; set; }
    
    private string _title = string.Empty;
    private InvoiceDetailDto _invoiceDetail = new();

    protected override async Task OnInitializedAsync()
    {
        if (InvoiceId == 0)
        {
            _title = "Rechnung nicht gefunden";
        }
        else
        {
            _title = $"Rechnung {InvoiceId}";

            var result = await HttpService.GetAsync<Result<InvoiceDetailDto>>($"/api/v1/Invoices/{InvoiceId}");

            if (result != null && result.Succeeded)
            {
                _title = $"Rechnung {_invoiceDetail.InvoiceNumber}";
                _invoiceDetail = result.Data;
            }
        }
    }
} 