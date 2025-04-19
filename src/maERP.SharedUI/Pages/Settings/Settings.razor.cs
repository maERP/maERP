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

    public List<Setting>? Settings { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await HttpService.GetAsync<Result<List<Setting>>>("/api/v1/Settings");
        
        if (result != null && result.Succeeded)
        {
            Settings = result.Data;
        }
        else
        {
            Snackbar.Add("Fehler beim Laden der Einstellungen", Severity.Error);
        }
    }

    private async Task Save()
    {
        if (Settings == null || !Settings.Any()) return;

        if (Form != null)
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                var result = await HttpService.PutAsync<Result<List<Setting>>>("/api/v1/Settings", Settings);

                if (result != null && result.Succeeded)
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
