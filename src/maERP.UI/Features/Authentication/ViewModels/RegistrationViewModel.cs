using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Authentication.ViewModels;

public partial class RegistrationViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;

    [ObservableProperty]
    [Required(ErrorMessage = "Vorname ist erforderlich")]
    private string firstName = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Nachname ist erforderlich")]
    private string lastName = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Email ist erforderlich")]
    [EmailAddress(ErrorMessage = "Ungültige Email-Adresse")]
    private string email = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Passwort ist erforderlich")]
    [MinLength(8, ErrorMessage = "Passwort muss mindestens 8 Zeichen lang sein")]
    private string password = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Passwort-Bestätigung ist erforderlich")]
    private string confirmPassword = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Server-URL ist erforderlich")]
    [Url(ErrorMessage = "Ungültige Server-URL")]
    private string serverUrl = "https://localhost:8443";

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string? errorMessage;

    public RegistrationViewModel(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Die Passwörter stimmen nicht überein";
                return;
            }

            var result = await _authenticationService.RegisterAsync(FirstName, LastName, Email, Password, ServerUrl);

            if (result.Succeeded)
            {
                OnRegistrationSuccessful?.Invoke();
            }
            else
            {
                ErrorMessage = result.Message ?? "Registrierung fehlgeschlagen";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler bei der Registrierung: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void BackToLogin()
    {
        OnBackToLogin?.Invoke();
    }

    public event Action? OnRegistrationSuccessful;
    public event Action? OnBackToLogin;
}