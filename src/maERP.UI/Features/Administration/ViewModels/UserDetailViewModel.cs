using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.User;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Administration.ViewModels;

public partial class UserDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

    [ObservableProperty]
    private UserDetailDto user = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string userId = string.Empty;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage);
    public string FullName => !string.IsNullOrEmpty(User.Firstname) && !string.IsNullOrEmpty(User.Lastname) 
        ? $"{User.Firstname} {User.Lastname}" 
        : User.Email;
    public bool HasEmail => !string.IsNullOrEmpty(User.Email);

    public Action? GoBackAction { get; set; }
    public Func<string, Task>? NavigateToEditUser { get; set; }

    public UserDetailViewModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task InitializeAsync(string userId)
    {
        UserId = userId;
        await LoadUserAsync();
    }

    [RelayCommand]
    private async Task LoadUserAsync()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            ErrorMessage = "Benutzer-ID ist erforderlich";
            return;
        }

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<UserDetailDto>($"users/{UserId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                System.Diagnostics.Debug.WriteLine("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                User = result.Data;
                OnPropertyChanged(nameof(FullName));
                OnPropertyChanged(nameof(HasEmail));
                System.Diagnostics.Debug.WriteLine($"Loaded user {User.Email} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.Count > 0 ? result.Messages[0] : "Fehler beim Laden des Benutzers";
                System.Diagnostics.Debug.WriteLine($"Failed to load user: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Benutzers: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception loading user: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadUserAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditUser()
    {
        if (NavigateToEditUser != null && !string.IsNullOrEmpty(UserId))
        {
            await NavigateToEditUser(UserId);
        }
    }

    [RelayCommand]
    private void SendEmail()
    {
        if (HasEmail)
        {
            try
            {
                var emailUri = $"mailto:{User.Email}";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = emailUri,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to open email client: {ex.Message}");
            }
        }
    }
}