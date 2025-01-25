using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.TaxClass;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ITaxClassService TaxClassService { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Parameter]
    public int taxClassId { get; set; }

    // ReSharper disable once NotAccessedField.Local
    private MudForm _form = new();

    protected string Title = "hinzufügen";

    protected TaxClassVm TaxClass = new();

    protected override async Task OnParametersSetAsync()
    {
        if (taxClassId != 0)
        {
            Title = "Bearbeiten";
            TaxClass = await TaxClassService.GetTaxClassDetails(taxClassId);
        }
    }

    protected async Task Save()
    {
        if (taxClassId != 0)
        {
            await TaxClassService.UpdateTaxClass(taxClassId, TaxClass);
        }
        else
        {
            await TaxClassService.CreateTaxClass(TaxClass);
        }

        Snackbar.Add("Steuerklasse gespeichert", Severity.Success);

        Cancel();
    }

    public void Cancel()
    {
        NavigationManager.NavigateTo("/TaxClasses");
    }
}