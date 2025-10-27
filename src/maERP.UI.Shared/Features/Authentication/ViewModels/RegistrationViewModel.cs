using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Shared.Services;
using maERP.UI.Shared.Shared.ViewModels;

namespace maERP.UI.Shared.Features.Authentication.ViewModels;

public partial class RegistrationViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;

    [ObservableProperty]
    [Required(ErrorMessage = "Vorname ist erforderlich")]
    [MinLength(2, ErrorMessage = "Vorname muss mindestens 2 Zeichen lang sein")]
    [MaxLength(100, ErrorMessage = "Vorname darf maximal 100 Zeichen lang sein")]
    [NotifyDataErrorInfo]
    private string firstName = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Nachname ist erforderlich")]
    [MinLength(2, ErrorMessage = "Nachname muss mindestens 2 Zeichen lang sein")]
    [MaxLength(100, ErrorMessage = "Nachname darf maximal 100 Zeichen lang sein")]
    [NotifyDataErrorInfo]
    private string lastName = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Email ist erforderlich")]
    [EmailAddress(ErrorMessage = "Ungültige Email-Adresse")]
    [MaxLength(255, ErrorMessage = "Email darf maximal 255 Zeichen lang sein")]
    [NotifyDataErrorInfo]
    private string email = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Passwort ist erforderlich")]
    [MinLength(8, ErrorMessage = "Passwort muss mindestens 8 Zeichen lang sein")]
    [MaxLength(100, ErrorMessage = "Passwort darf maximal 100 Zeichen lang sein")]
    [NotifyDataErrorInfo]
    private string password = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Passwort-Bestätigung ist erforderlich")]
    [NotifyDataErrorInfo]
    private string confirmPassword = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Server-URL ist erforderlich")]
    [Url(ErrorMessage = "Ungültige Server-URL")]
    [NotifyDataErrorInfo]
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

            // Validate form inputs
            if (!ValidateForm())
            {
                return;
            }

            // Check password confirmation
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

    private bool ValidateForm()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Eingabefehler";
            return false;
        }

        return true;
    }

    [RelayCommand]
    private void BackToLogin()
    {
        OnBackToLogin?.Invoke();
    }

    public event Action? OnRegistrationSuccessful;
    public event Action? OnBackToLogin;
}