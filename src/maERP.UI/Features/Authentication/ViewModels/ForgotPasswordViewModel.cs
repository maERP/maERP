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

public partial class ForgotPasswordViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly HttpClient _httpClient;

    [ObservableProperty]
    [Required(ErrorMessage = "E-Mail-Adresse ist erforderlich")]
    [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse")]
    private string email = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string? errorMessage;

    [ObservableProperty]
    private string? successMessage;

    [ObservableProperty]
    private string serverUrl = "https://localhost:8443";

    public ForgotPasswordViewModel(IHttpService httpService, IDebugService debugService, HttpClient httpClient)
    {
        _httpService = httpService;
        _debugService = debugService;
        _httpClient = httpClient;
    }

    public void SetServerUrl(string url)
    {
        ServerUrl = url;
        _debugService.LogInfo($"🔧 ForgotPassword ServerUrl set to: {url}");
    }

    [RelayCommand]
    private async Task RequestPasswordResetAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;
            SuccessMessage = null;

            // Validate email
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Bitte geben Sie Ihre E-Mail-Adresse ein.";
                return;
            }

            // Validate ServerUrl
            if (string.IsNullOrWhiteSpace(ServerUrl))
            {
                ErrorMessage = "Server-URL ist nicht gesetzt.";
                return;
            }

            _debugService.LogInfo($"🔄 Sending forgot-password request to {ServerUrl}");

            // Send request to backend directly
            var request = new
            {
                email = Email
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(request, jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{ServerUrl.TrimEnd('/')}/api/v1/auth/forgot-password";
            _debugService.LogInfo($"🌐 Full URL: {url}");

            var httpResponse = await _httpClient.PostAsync(url, content);

            _debugService.LogInfo($"📡 Response status: {httpResponse.StatusCode}");

            if (httpResponse.IsSuccessStatusCode)
            {
                SuccessMessage = "Falls ein Konto mit dieser E-Mail-Adresse existiert, wurde eine E-Mail mit Anweisungen zum Zurücksetzen des Passworts gesendet.";
                Email = string.Empty; // Clear email for security
                _debugService.LogInfo("✅ Forgot-password request successful");
            }
            else
            {
                var errorContent = await httpResponse.Content.ReadAsStringAsync();
                _debugService.LogError($"❌ Forgot-password request failed: {httpResponse.StatusCode} - {errorContent}");
                ErrorMessage = "Es ist ein Fehler aufgetreten. Bitte versuchen Sie es später erneut.";
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError($"❌ Exception in forgot-password: {ex.Message}");
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
}
