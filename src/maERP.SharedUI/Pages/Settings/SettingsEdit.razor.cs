using System.Net.Http.Json;
using maERP.Domain.Dtos.Settings;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Settings;

public partial class SettingsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int Id { get; set; }
    
    public MudForm? Form;

    private SettingInputDto _setting = new();

    protected override async Task OnInitializedAsync()
    {
        if (Id == 0)
        {
            // New setting
        }
        else
        {
            var result = await HttpService.GetAsync<Result<SettingInputDto>>($"/api/v1/Settings/{Id}");
            
            if (result != null && result.Succeeded)
            {
                _setting = result.Data;
            }
        }
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        if (Id == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/Warehouses", _setting);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/Warehouses/{Id}", _setting);
        }

        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("Lager gespeichert", Severity.Success);
                NavigationManager.NavigateTo("/Settings");
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
}
