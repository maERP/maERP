using maERP.Domain.Dtos.TaxClass;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
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
    public required TaxClassUpdateValidator Validator { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Parameter]
    public int taxClassId { get; set; }
    
    public MudForm _form = new();

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
    
    protected async Task OnValidSubmit()
    {
        if (_form is not null)
        {
            await _form.Validate();
            
            if (_form.IsValid)
            {
                await Save();
            }
        }
    }

    protected async Task Save()
    {
        if (taxClassId != 0)
        {
            await HttpService.PutAsJsonAsync<TaxClassDetailDto>($"/api/v1/TaxClasses/{taxClassId}", TaxClass);
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