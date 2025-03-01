using System.Net.Http.Json;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.SalesChannels;

public partial class SalesChannelsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required SalesChannelUpdateValidator Validator { get; set; }

    [Parameter]
    public int salesChannelId { get; set; }
    
    public MudForm? _form;
    
    protected string Title = "Bearbeiten";

    public SalesChannelUpdateDto SalesChannel = new();
    public List<WarehouseListDto> Warehouses = new();

    protected override async Task OnParametersSetAsync()
    {
        if (salesChannelId != 0)
        {
            var result = await HttpService.GetAsync<Result<SalesChannelUpdateDto>>($"/api/v1/SalesChannels/{salesChannelId}");
            
            if (result != null && result.Succeeded)
            {
                SalesChannel = result.Data;
            }

            var warehouseResult = await HttpService.GetAsync<PaginatedResult<WarehouseListDto>>("/api/v1/Warehouses") ??
                         throw new Exception();
            Warehouses = warehouseResult.Data;
        }
    }

    protected async Task Save()
    {
        var httpResponseMessage = await HttpService.PutAsJsonAsync<SalesChannelUpdateDto>($"/api/v1/SalesChannels/{salesChannelId}", SalesChannel);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;

        if (result != null)
        {
            if (result.Succeeded)
            {
                NavigateToList();
                Snackbar.Add("Vertriebskanal gespeichert", Severity.Success);
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