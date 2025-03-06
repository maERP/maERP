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
    public required SalesChannelInputValidator Validator { get; set; }

    [Parameter]
    public int salesChannelId { get; set; }
    
    public MudForm? Form;

    protected string Title = string.Empty;

    public SalesChannelInputDto SalesChannel = new();
    public List<WarehouseListDto> Warehouses = new();

    protected override async Task OnInitializedAsync()
    {
        var warehouseResult = await HttpService.GetAsync<PaginatedResult<WarehouseListDto>>("/api/v1/Warehouses") ?? throw new Exception();
        Warehouses = warehouseResult.Data;
        
        if (salesChannelId == 0)
        {
            Title = "Vertriebskanal hinzufügen";
        }
        else
        {
            Title = "Vertriebskanal bearbeiten";
            
            var result = await HttpService.GetAsync<Result<SalesChannelInputDto>>($"/api/v1/SalesChannels/{salesChannelId}");
            
            if (result != null && result.Succeeded)
            {
                SalesChannel = result.Data;
            }
        }
        
        StateHasChanged();
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        if (salesChannelId == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/SalesChannels", SalesChannel);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/SalesChannels/{salesChannelId}", SalesChannel);
        }

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
                    Snackbar.Add("SERVER: " + errorMessage, Severity.Error);
                }
            }
        }
        else
        {
            Snackbar.Add("Vertriebskanal konnte nicht gespeichert werden", Severity.Error);
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
        NavigationManager.NavigateTo("/SalesChannels");
    }
}