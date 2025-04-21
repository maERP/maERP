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

    private async Task Save()
    {
        if (Form != null)
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                Result<int>? result;
                if (Id == 0)
                {
                    result = await HttpService.PostAsync<Result<int>>("/api/v1/Settings", _setting);
                }
                else
                {
                    result = await HttpService.PutAsync<Result<int>>($"/api/v1/Settings/{Id}", _setting);
                }

                if (result != null && result.Succeeded)
                {
                    Snackbar.Add("Einstellung erfolgreich gespeichert", Severity.Success);
                    NavigationManager.NavigateTo("/Settings");
                }
                else
                {
                    Snackbar.Add("Fehler beim Speichern der Einstellung", Severity.Error);
                }
            }
        }
    }
}
