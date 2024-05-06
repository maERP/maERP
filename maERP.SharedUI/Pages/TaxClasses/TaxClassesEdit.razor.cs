using maERP.SharedUI.Models.TaxClass;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesEdit
{

    [Parameter]
    public int taxClassId { get; set; }

    private MudForm _form = new();

    protected string Title = "hinzufügen";

    protected TaxClassVM taxClass = new();

    protected override async Task OnParametersSetAsync()
    {
        if (taxClassId != 0)
        {
            Title = "Bearbeiten";
            taxClass = await _taxClassService.GetTaxClassDetails(taxClassId);
        }
    }

    protected async Task Save()
    {
        if (taxClassId != 0)
        {
            await _taxClassService.UpdateTaxClass(taxClassId, taxClass);
        }
        else
        {
            await _taxClassService.CreateTaxClass(taxClass);
        }

        _snackbar.Add("Steuerklasse gespeichert", Severity.Success);

        Cancel();
    }

    public void Cancel()
    {
        _navigationManager.NavigateTo("/TaxClasses");
    }
}