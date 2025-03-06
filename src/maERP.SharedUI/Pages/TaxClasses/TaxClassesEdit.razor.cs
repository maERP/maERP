using System.Net.Http.Json;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
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
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required TaxClassInputValidator Validator { get; set; }

    [Parameter]
    public int taxClassId { get; set; }
    
    public MudForm? Form;

    protected string Title = string.Empty;

    public TaxClassInputDto TaxClass = new();

    protected override async Task OnInitializedAsync()
    {
        if (taxClassId == 0)
        {
            Title = "Steuersatz hinzufügen";
        }
        else
        {
            Title = "Steuersatz bearbeiten";
            
            var result = await HttpService.GetAsync<Result<TaxClassInputDto>>($"/api/v1/TaxClasses/{taxClassId}");
            
            if (result != null && result.Succeeded)
            {
                TaxClass = result.Data;
            }
        }
        
        StateHasChanged();
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        if (taxClassId == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/TaxClasses", TaxClass);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/TaxClasses/{taxClassId}", TaxClass);
        }

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

    protected async Task OnValidSubmit()
    {
        if (Form is not null)
        {
            await Form.Validate();
            
            if (Form.IsValid)
            {
                await Save();
            }
        }
    }
    
    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/TaxClasses");
    }
}