using System.Net.Http.Json;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Warehouses;

public partial class WarehouseEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required WarehouseUpdateValidator Validator { get; set; }

    [Parameter]
    public int warehouseId { get; set; }
    
    public MudForm? _form;
    
    protected string Title = "Bearbeiten";

    public WarehouseUpdateDto Warehouse = new();

    protected override async Task OnParametersSetAsync()
    {
        if (warehouseId != 0)
        {
            var result = await HttpService.GetAsync<Result<WarehouseUpdateDto>>($"/api/v1/Warehouses/{warehouseId}");
            
            if (result != null && result.Succeeded)
            {
                Warehouse = result.Data;
            }
            else
            {
                // Handle error case
                Warehouse = new();
            }
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
        var httpResponseMessage = await HttpService.PutAsJsonAsync<WarehouseUpdateDto>($"/api/v1/AiModels/{warehouseId}", Warehouse);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;

        if (result != null)
        {
            if (result.Succeeded)
            {
                NavigateToList();
                Snackbar.Add("Lager gespeichert", Severity.Success);
            }
            else
            {
                foreach (var errorMessage in result.Messages)
                {
                    Snackbar.Add(errorMessage, Severity.Error);
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