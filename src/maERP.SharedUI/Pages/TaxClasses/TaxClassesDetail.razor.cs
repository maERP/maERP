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
    public int taxClassId { get; set; }

    protected string Title = "Steuers�tze";

    protected TaxClassDetailDto TaxClass = new();

    protected override async Task OnParametersSetAsync()
    {
        if (taxClassId != 0)
        {
            Title = "Bearbeiten";
            TaxClass = await HttpService.GetAsync<TaxClassDetailDto>($"/api/v1/TaxClasses/{taxClassId}") ?? new TaxClassDetailDto();
        }
        else Title = "nicht gefunden";

        await Task.CompletedTask;
    }
}