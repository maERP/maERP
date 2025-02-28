using System.Net.Http.Json;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannelCreate
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }
    
    [Inject]
    public required SalesChannelCreateValidator Validator { get; set; }
    
    private MudForm? _form;
    
    public SalesChannelCreateDto SalesChannel = new();
    public List<WarehouseListDto> Warehouses = new();
    
    protected override async Task OnParametersSetAsync()
    {
        var result = await HttpService.GetAsync<PaginatedResult<WarehouseListDto>>("/api/v1/Warehouses") ??
                     throw new Exception();
        Warehouses = result.Data;
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
        var httpResponseMessage = await HttpService.PostAsJsonAsync<SalesChannelCreateDto>("/api/v1/SalesChannels", SalesChannel);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("Vertriebskanal gespeichert", Severity.Success);
                NavigateToList();
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
            Snackbar.Add("Vertriebskanal konnte nicht gespeichert werden", Severity.Error);
        }
    }

    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/SalesChannels");
    }
}
