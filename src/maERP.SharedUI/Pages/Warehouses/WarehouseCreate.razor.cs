using System.Net.Http.Json;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehouseCreate
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }
    
    [Inject]
    public required WarehouseCreateValidator Validator { get; set; }
    
    private MudForm? _form;
    
    public WarehouseCreateDto Warehouse = new();
    
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
        var httpResponseMessage = await HttpService.PostAsJsonAsync<WarehouseCreateDto>("/api/v1/Warehouses", Warehouse);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("Lager gespeichert", Severity.Success);
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
            Snackbar.Add("Lager konnte nicht gespeichert werden", Severity.Error);
        }
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/Warehouses");
    }
}
