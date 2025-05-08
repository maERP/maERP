using maERP.Domain.Dtos.TaxClass;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int TaxClassId { get; set; }

    private string _title = "Steuersätze";

    private TaxClassDetailDto _taxClassDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        if (TaxClassId != 0)
        {
            _title = "Bearbeiten";
            _taxClassDetail = await HttpService.GetAsync<TaxClassDetailDto>($"/api/v1/TaxClasses/{TaxClassId}") ?? new TaxClassDetailDto();
        }
        else _title = "nicht gefunden";

        await Task.CompletedTask;
    }
}