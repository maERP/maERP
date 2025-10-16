using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Authentication.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ISettingsService _settingsService;

    private const string DebugEmail = "admin@localhost.com";
    private const string DebugPassword = "P@ssword1";

    [ObservableProperty]
    [Required(ErrorMessage = "Email ist erforderlich")]
    [EmailAddress(ErrorMessage = "Ungültige Email-Adresse")]
    private string email = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Passwort ist erforderlich")]
    [MinLength(8, ErrorMessage = "Passwort muss mindestens 8 Zeichen lang sein")]
    private string password = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Server-URL ist erforderlich")]
    [Url(ErrorMessage = "Ungültige Server-URL")]
    private string serverUrl = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string? errorMessage;

    [ObservableProperty]
    private bool isLoggedIn;

    [ObservableProperty]
    private bool rememberMe;

    public LoginViewModel(IAuthenticationService authenticationService, ISettingsService settingsService)
    {
        _authenticationService = authenticationService;
        _settingsService = settingsService;

        // Set default server URL based on environment
        ServerUrl = Debugger.IsAttached ? "https://localhost:8443" : "https://www.maerp.de/";

        if (Debugger.IsAttached)
        {
            Email = DebugEmail;
            Password = DebugPassword;
        }

        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Load saved settings as baseline
        RememberMe = await _settingsService.GetRememberMeAsync();

        if (Debugger.IsAttached)
        {
            // Keep debug defaults and skip loading stored credentials
            return;
        }

        if (RememberMe)
        {
            var savedCredentials = await _settingsService.LoadLoginCredentialsAsync();
            if (savedCredentials.HasValue)
            {
                Email = savedCredentials.Value.Email;
                Password = savedCredentials.Value.Password;
                ServerUrl = savedCredentials.Value.ServerUrl;
            }
        }
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;

            var result = await _authenticationService.LoginAsync(Email, Password, ServerUrl);

            if (result.Succeeded)
            {
                // Speichere RememberMe-Einstellung
                await _settingsService.SetRememberMeAsync(RememberMe);

                // Speichere Credentials wenn RememberMe aktiviert ist
                if (RememberMe)
                {
                    await _settingsService.SaveLoginCredentialsAsync(Email, Password, ServerUrl);
                }
                else
                {
                    await _settingsService.ClearLoginCredentialsAsync();
                }

                IsLoggedIn = true;
                OnLoginSuccessful?.Invoke();
            }
            else
            {
                ErrorMessage = result.Message ?? "Login fehlgeschlagen";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Login: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void ShowRegistration()
    {
        OnShowRegistration?.Invoke();
    }

    [RelayCommand]
    private void ShowForgotPassword()
    {
        OnShowForgotPassword?.Invoke();
    }

    public event Action? OnLoginSuccessful;
    public event Action? OnShowRegistration;
    public event Action? OnShowForgotPassword;

    public async Task<bool> TryAutoLoginAsync()
    {
        try
        {
            var rememberMe = await _settingsService.GetRememberMeAsync();
            if (!rememberMe)
                return false;

            var savedCredentials = await _settingsService.LoadLoginCredentialsAsync();
            if (!savedCredentials.HasValue)
                return false;

            var credentials = savedCredentials.Value;
            Email = credentials.Email;
            Password = credentials.Password;
            ServerUrl = credentials.ServerUrl;
            RememberMe = true;

            var result = await _authenticationService.LoginAsync(credentials.Email, credentials.Password, credentials.ServerUrl);

            if (result.Succeeded)
            {
                IsLoggedIn = true;
                return true;
            }
            else
            {
                // Wenn Auto-Login fehlschlägt, lösche gespeicherte Credentials
                await _settingsService.ClearLoginCredentialsAsync();
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
}
