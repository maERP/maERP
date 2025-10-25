using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Authentication.ViewModels;

public partial class ResetPasswordViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly HttpClient _httpClient;

    [ObservableProperty]
    [Required(ErrorMessage = "E-Mail-Adresse ist erforderlich")]
    [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse")]
    private string email = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Token ist erforderlich")]
    private string token = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Neues Passwort ist erforderlich")]
    [MinLength(6, ErrorMessage = "Passwort muss mindestens 6 Zeichen lang sein")]
    private string newPassword = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Passwort-Bestätigung ist erforderlich")]
    private string confirmPassword = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string? errorMessage;

    [ObservableProperty]
    private string? successMessage;

    [ObservableProperty]
    private string serverUrl = "https://localhost:8443";

    public ResetPasswordViewModel(IHttpService httpService, IDebugService debugService, HttpClient httpClient)
    {
        _httpService = httpService;
        _debugService = debugService;
        _httpClient = httpClient;
    }

    public void SetServerUrl(string url)
    {
        ServerUrl = url;
        _debugService.LogInfo($"🔧 ResetPassword ServerUrl set to: {url}");
    }

    public void SetTokenFromUrl(string? token, string? email)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            Token = token;
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            Email = email;
        }
    }

    [RelayCommand]
    private async Task ResetPasswordAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;
            SuccessMessage = null;

            // Validate
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Bitte geben Sie Ihre E-Mail-Adresse ein.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Token))
            {
                ErrorMessage = "Kein Reset-Token vorhanden. Bitte fordern Sie einen neuen Link an.";
                return;
            }

            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                ErrorMessage = "Bitte geben Sie ein neues Passwort ein.";
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                ErrorMessage = "Die Passwörter stimmen nicht überein.";
                return;
            }

            if (NewPassword.Length < 6)
            {
                ErrorMessage = "Das Passwort muss mindestens 6 Zeichen lang sein.";
                return;
            }

            // Validate ServerUrl
            if (string.IsNullOrWhiteSpace(ServerUrl))
            {
                ErrorMessage = "Server-URL ist nicht gesetzt.";
                return;
            }

            _debugService.LogInfo($"🔄 Sending reset-password request to {ServerUrl}");

            // Send request to backend directly
            var request = new
            {
                email = Email,
                token = Token,
                newPassword = NewPassword,
                confirmPassword = ConfirmPassword
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(request, jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ServerUrl.TrimEnd('/')}/api/v1/auth/reset-password";
            _debugService.LogInfo($"🌐 Full URL: {url}");

            var httpResponse = await _httpClient.PostAsync(url, content);

            _debugService.LogInfo($"📡 Response status: {httpResponse.StatusCode}");

            if (httpResponse.IsSuccessStatusCode)
            {
                SuccessMessage = "Ihr Passwort wurde erfolgreich zurückgesetzt. Sie können sich jetzt mit Ihrem neuen Passwort anmelden.";

                // Clear sensitive data
                NewPassword = string.Empty;
                ConfirmPassword = string.Empty;
                Token = string.Empty;

                _debugService.LogInfo("✅ Reset-password request successful");

                // Navigate back to login after short delay
                await Task.Delay(2000);
                OnPasswordResetSuccess?.Invoke();
            }
            else
            {
                var errorContent = await httpResponse.Content.ReadAsStringAsync();
                _debugService.LogError($"❌ Reset-password request failed: {httpResponse.StatusCode} - {errorContent}");
                ErrorMessage = "Das Zurücksetzen des Passworts ist fehlgeschlagen. Bitte überprüfen Sie Ihre Eingaben.";
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError($"❌ Exception in reset-password: {ex.Message}");
            _debugService.LogError($"Stack trace: {ex.StackTrace}");
            ErrorMessage = $"Fehler: {ex.Message}";
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

    public event Action? OnBackToLogin;
    public event Action? OnPasswordResetSuccess;
}
