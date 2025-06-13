using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.User;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Administration.ViewModels;

public partial class UserInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private string userId = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Vorname ist erforderlich")]
    [NotifyDataErrorInfo]
    private string firstname = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Nachname ist erforderlich")]
    [NotifyDataErrorInfo]
    private string lastname = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "E-Mail ist erforderlich")]
    [EmailAddress(ErrorMessage = "E-Mail-Adresse ist ungültig")]
    [NotifyDataErrorInfo]
    private string email = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string passwordConfirm = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    public bool IsEditMode => !string.IsNullOrEmpty(UserId);
    public string PageTitle => IsEditMode ? "Benutzer bearbeiten" : "Neuen Benutzer erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);
    public string FullName => !string.IsNullOrEmpty(Firstname) && !string.IsNullOrEmpty(Lastname) 
        ? $"{Firstname} {Lastname}" 
        : "Neuer Benutzer";
    public bool IsPasswordRequired => !IsEditMode;
    public string PasswordHint => IsEditMode 
        ? "Lassen Sie die Passwort-Felder leer, um das Passwort nicht zu ändern"
        : "Passwort muss mindestens 8 Zeichen haben";

    public Action? GoBackAction { get; set; }
    public Func<string, Task>? NavigateToUserDetail { get; set; }

    public UserInputViewModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task InitializeAsync(string userId = "")
    {
        UserId = userId;
        
        if (IsEditMode)
        {
            await LoadAsync();
        }
        else
        {
            ClearForm();
        }
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (string.IsNullOrEmpty(UserId)) return;

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
                var user = result.Data;
                Firstname = user.Firstname;
                Lastname = user.Lastname;
                Email = user.Email;
                Password = string.Empty;
                PasswordConfirm = string.Empty;
                
                OnPropertyChanged(nameof(FullName));
                System.Diagnostics.Debug.WriteLine($"Loaded user {UserId} for editing");
            }
            else
            {
                ErrorMessage = result.Messages?.Count > 0 ? result.Messages[0] : $"Fehler beim Laden des Benutzers {UserId}";
                System.Diagnostics.Debug.WriteLine($"Failed to load user {UserId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Benutzers: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception loading user {UserId}: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!ValidateForm()) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            if (IsEditMode)
            {
                var updateDto = new UserUpdateDto
                {
                    Firstname = Firstname,
                    Lastname = Lastname,
                    Email = Email,
                    Password = Password,
                    PasswordConfirm = PasswordConfirm
                };

                var result = await _httpService.PutAsync<UserUpdateDto, string>($"users/{UserId}", updateDto);

                if (result == null)
                {
                    ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                    System.Diagnostics.Debug.WriteLine("SaveAsync returned null - not authenticated or no server URL");
                }
                else if (result.Succeeded)
                {
                    System.Diagnostics.Debug.WriteLine($"User {UserId} updated successfully");
                    
                    if (NavigateToUserDetail != null)
                    {
                        await NavigateToUserDetail(result.Data);
                    }
                    else
                    {
                        GoBackAction?.Invoke();
                    }
                }
                else
                {
                    ErrorMessage = result.Messages?.Count > 0 ? result.Messages[0] : "Fehler beim Aktualisieren des Benutzers";
                    System.Diagnostics.Debug.WriteLine($"Failed to update user: {ErrorMessage}");
                }
            }
            else
            {
                var createDto = new UserCreateDto
                {
                    Firstname = Firstname,
                    Lastname = Lastname,
                    Email = Email,
                    Password = Password,
                    PasswordConfirm = PasswordConfirm
                };

                var result = await _httpService.PostAsync<UserCreateDto, string>("users", createDto);

                if (result == null)
                {
                    ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                    System.Diagnostics.Debug.WriteLine("SaveAsync returned null - not authenticated or no server URL");
                }
                else if (result.Succeeded)
                {
                    System.Diagnostics.Debug.WriteLine($"User created successfully");
                    
                    if (NavigateToUserDetail != null)
                    {
                        await NavigateToUserDetail(result.Data);
                    }
                    else
                    {
                        GoBackAction?.Invoke();
                    }
                }
                else
                {
                    ErrorMessage = result.Messages?.Count > 0 ? result.Messages[0] : "Fehler beim Erstellen des Benutzers";
                    System.Diagnostics.Debug.WriteLine($"Failed to create user: {ErrorMessage}");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern des Benutzers: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception saving user: {ex}");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private void ClearForm()
    {
        Firstname = string.Empty;
        Lastname = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        PasswordConfirm = string.Empty;
        ErrorMessage = string.Empty;
        OnPropertyChanged(nameof(FullName));
        ClearErrors();
    }

    private bool ValidateForm()
    {
        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Validierungsfehler.";
            return false;
        }

        // Additional validation for passwords
        if (!IsEditMode || (!string.IsNullOrEmpty(Password) || !string.IsNullOrEmpty(PasswordConfirm)))
        {
            if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Passwort ist erforderlich.";
                return false;
            }

            if (Password.Length < 8)
            {
                ErrorMessage = "Passwort muss mindestens 8 Zeichen haben.";
                return false;
            }

            if (Password != PasswordConfirm)
            {
                ErrorMessage = "Die Passwörter stimmen nicht überein.";
                return false;
            }
        }

        return true;
    }
}