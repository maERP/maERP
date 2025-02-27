using System.Net.Http.Json;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClassesCreate
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }
    
    [Inject]
    public required TaxClassCreateValidator Validator { get; set; }

    // ReSharper disable once NotAccessedField.Local
    private MudForm? _form;

    public TaxClassCreateDto TaxClassCreate = new();
    
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
        var httpResponseMessage = await HttpService.PostAsJsonAsync<TaxClassCreateDto>("/api/v1/TaxClasses", TaxClassCreate);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("Steuersatz gespeichert", Severity.Success);
                NavigateToList();
            }
            else
            {
                foreach (var errorMessage in result.Messages)
                {
                    Snackbar.Add("SERVER: " + errorMessage, Severity.Error);
                }
            }
        }
        else
        {
            Snackbar.Add("Steuersatz konnte nicht gespeichert werden", Severity.Error);
        }
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/TaxClasses");
    }
}
