using maERP.Domain.Dtos.TaxClass;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Parameter]
    public int taxClassId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    private MudForm _form = new();

    protected string Title = "hinzufügen";

    protected TaxClassDetailDto TaxClass = new();

    protected override async Task OnParametersSetAsync()
    {
        if (taxClassId != 0)
        {
            Title = "Bearbeiten";
            TaxClass = await HttpService.GetAsync<TaxClassDetailDto>($"/api/v1/TaxClasses/{taxClassId}") ?? new TaxClassDetailDto();
        }
    }

    protected async Task Save()
    {
        if (taxClassId != 0)
        {
            await HttpService.PutAsJsonAsync<TaxClassDetailDto>("/api/v1/TaxClasses/{taxClassId}", TaxClass);
        }
        else
        {
            await HttpService.PostAsJsonAsync<TaxClassDetailDto>("/api/v1/TaxClasses/", TaxClass);
        }

        Snackbar.Add("Steuerklasse gespeichert", Severity.Success);

        Cancel();
    }

    public void Cancel()
    {
        NavigationManager.NavigateTo("/TaxClasses");
    }
}