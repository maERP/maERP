using System.Net.Http.Json;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehousesEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required WarehouseInputValidator Validator { get; set; }

    [Parameter]
    public int warehouseId { get; set; }
    
    public MudForm? Form;

    protected string Title = string.Empty;

    public WarehouseInputDto Warehouse = new();

    protected override async Task OnInitializedAsync()
    {
        if (warehouseId == 0)
        {
            Title = "Lager hinzufügen";
        }
        else
        {
            Title = "Lager bearbeiten";
            
            var result = await HttpService.GetAsync<Result<WarehouseInputDto>>($"/api/v1/Warehouses/{warehouseId}");
            
            if (result != null && result.Succeeded)
            {
                Warehouse = result.Data;
            }
        }
        
        StateHasChanged();
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        if (warehouseId == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/Warehouses", Warehouse);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/Warehouses/{warehouseId}", Warehouse);
        }

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
        NavigationManager.NavigateTo("/Warehouses");
    }
}