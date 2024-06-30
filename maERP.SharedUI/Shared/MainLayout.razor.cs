using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Shared;

public partial class MainLayout : LayoutComponentBase
{
    public MudThemeProvider _mudThemeProvider = null!;
    public bool _defaultDarkMode;

    public string PrimaryColor { get; set; } = "#2d4275";
    public string SecondaryColor { get; set; } = "#ff4081ff";
    public double BorderRadius { get; set; } = 4;
    public double DefaultFontSize { get; set; } = 0.8125;
    public MudTheme CurrentTheme { get; private set; } = new();
    public event EventHandler? MajorUpdateOccured;

    private void OnMajorUpdateOccured() => MajorUpdateOccured?.Invoke(this, EventArgs.Empty);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await _mudThemeProvider.WatchSystemPreference(OnSystemPreferenceChanged);
            StateHasChanged();
        }
    }

    private async Task OnSystemPreferenceChanged(bool newValue)
    {
        _defaultDarkMode = newValue;
        await Task.CompletedTask;
    }

    protected override void OnInitialized()
    {
        _defaultDarkMode = false; //  await _mudThemeProvider.GetSystemPreference();

        CurrentTheme = Theme.ApplicationTheme();
        CurrentTheme.PaletteLight.Primary = PrimaryColor;
        CurrentTheme.PaletteDark.Primary = PrimaryColor;
        CurrentTheme.LayoutProperties.DefaultBorderRadius = BorderRadius + "px";

        OnMajorUpdateOccured();
    }

    public async void IsDarkMode()
    {
        _defaultDarkMode = await _mudThemeProvider.GetSystemPreference();
    }
}