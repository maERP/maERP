using maERP.Domain.Dtos.Setting;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Settings;

public partial class Settings
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    public MudForm? Form;

    private List<SettingListDto>? _settings { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await HttpService.GetAsync<Result<List<SettingListDto>>>("/api/v1/Settings");
        
        if (result != null && result.Succeeded)
        {
            _settings = result.Data;
        }
        else
        {
            Snackbar.Add("Fehler beim Laden der Einstellungen", Severity.Error);
        }
    }

    private async Task Save()
    {
        if (_settings == null || !_settings.Any()) return;

        if (Form != null)
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                var result = await HttpService.PutAsJsonAsync("/api/v1/Settings", _settings);

                if (result != null && result.IsSuccessStatusCode)
                {
                    Snackbar.Add("Einstellungen erfolgreich gespeichert", Severity.Success);
                    NavigationManager.NavigateTo("/Settings", true);
                }
                else
                {
                    Snackbar.Add("Fehler beim Speichern der Einstellungen", Severity.Error);
                }
            }
        }
    }
}
