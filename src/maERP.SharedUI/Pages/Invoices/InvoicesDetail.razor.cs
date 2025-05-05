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
    public int invoiceId { get; set; }

    protected string Title = string.Empty;

    protected InvoiceDetailDto Invoice = new();

    protected override async Task OnInitializedAsync()
    {
        if (invoiceId == 0)
        {
            Title = "Rechnung nicht gefunden";
        }
        else
        {
            Title = $"Rechnung {invoiceId}";

            var result = await HttpService.GetAsync<Result<InvoiceDetailDto>>($"/api/v1/Invoices/{invoiceId}");

            if (result != null && result.Succeeded)
            {
                Invoice = result.Data;
            }
        }
    }
} 