using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Account.Services;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Account.Models;

/// <summary>
/// Model for the "Mein Konto" page: edit own profile and change own password.
/// One page, two independent save actions.
/// </summary>
public class AccountModel : AsyncInitializableModel
{
    private readonly IAccountService _accountService;
    private readonly IStringLocalizer _localizer;
    private readonly ILogger<AccountModel> _logger;

    private string _email = string.Empty;
    private string _firstname = string.Empty;
    private string _lastname = string.Empty;
    private string _phoneNumber = string.Empty;

    private string _currentPassword = string.Empty;
    private string _newPassword = string.Empty;
    private string _newPasswordConfirm = string.Empty;

    private bool _isSavingProfile;
    private bool _isChangingPassword;

    private string _profileError = string.Empty;
    private string _profileSuccess = string.Empty;
    private string _passwsalesror = string.Empty;
    private string _passwordSuccess = string.Empty;

    public AccountModel(
        IAccountService accountService,
        IStringLocalizer localizer,
        ILogger<AccountModel> logger)
        : base(logger)
    {
        _accountService = accountService;
        _localizer = localizer;
        _logger = logger;

        StartInitialization();
    }

    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        var profile = await _accountService.GetCurrentUserAsync(ct);
        if (profile != null)
        {
            Email = profile.Email;
            Firstname = profile.Firstname;
            Lastname = profile.Lastname;
            PhoneNumber = profile.PhoneNumber;
        }
    }

    #region Profile fields

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string Firstname
    {
        get => _firstname;
        set => SetProperty(ref _firstname, value);
    }

    public string Lastname
    {
        get => _lastname;
        set => SetProperty(ref _lastname, value);
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => SetProperty(ref _phoneNumber, value);
    }

    #endregion

    #region Password fields

    public string CurrentPassword
    {
        get => _currentPassword;
        set => SetProperty(ref _currentPassword, value);
    }

    public string NewPassword
    {
        get => _newPassword;
        set => SetProperty(ref _newPassword, value);
    }

    public string NewPasswordConfirm
    {
        get => _newPasswordConfirm;
        set => SetProperty(ref _newPasswordConfirm, value);
    }

    #endregion

    #region UI state

    public bool IsSavingProfile
    {
        get => _isSavingProfile;
        private set
        {
            if (SetProperty(ref _isSavingProfile, value))
            {
                OnPropertyChanged(nameof(CanSaveProfile));
            }
        }
    }

    public bool IsChangingPassword
    {
        get => _isChangingPassword;
        private set
        {
            if (SetProperty(ref _isChangingPassword, value))
            {
                OnPropertyChanged(nameof(CanChangePassword));
            }
        }
    }

    public string ProfileError
    {
        get => _profileError;
        set => SetProperty(ref _profileError, value);
    }

    public string ProfileSuccess
    {
        get => _profileSuccess;
        set => SetProperty(ref _profileSuccess, value);
    }

    public string Passwsalesror
    {
        get => _passwsalesror;
        set => SetProperty(ref _passwsalesror, value);
    }

    public string PasswordSuccess
    {
        get => _passwordSuccess;
        set => SetProperty(ref _passwordSuccess, value);
    }

    public bool HasProfileError => !string.IsNullOrEmpty(ProfileError);
    public bool HasProfileSuccess => !string.IsNullOrEmpty(ProfileSuccess);
    public bool HasPasswsalesror => !string.IsNullOrEmpty(Passwsalesror);
    public bool HasPasswordSuccess => !string.IsNullOrEmpty(PasswordSuccess);

    public bool CanSaveProfile =>
        !IsSavingProfile &&
        !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(Firstname) &&
        !string.IsNullOrWhiteSpace(Lastname);

    public bool CanChangePassword =>
        !IsChangingPassword &&
        !string.IsNullOrWhiteSpace(CurrentPassword) &&
        !string.IsNullOrWhiteSpace(NewPassword) &&
        !string.IsNullOrWhiteSpace(NewPasswordConfirm);

    #endregion

    public async Task SaveProfileAsync(CancellationToken ct = default)
    {
        if (!CanSaveProfile) return;

        IsSavingProfile = true;
        ProfileError = string.Empty;
        ProfileSuccess = string.Empty;

        try
        {
            await _accountService.UpdateCurrentUserAsync(Email, Firstname, Lastname, PhoneNumber, ct);
            ProfileSuccess = _localizer["Account.Profile.Saved"];
        }
        catch (ApiException ex)
        {
            ProfileError = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while saving profile");
            ProfileError = string.Format(_localizer["Account.Profile.SaveFailed"], ex.Message);
        }
        finally
        {
            IsSavingProfile = false;
        }
    }

    public async Task ChangePasswordAsync(CancellationToken ct = default)
    {
        if (!CanChangePassword) return;

        IsChangingPassword = true;
        Passwsalesror = string.Empty;
        PasswordSuccess = string.Empty;

        if (NewPassword != NewPasswordConfirm)
        {
            Passwsalesror = _localizer["Account.Password.MismatchError"];
            IsChangingPassword = false;
            return;
        }

        try
        {
            await _accountService.ChangePasswordAsync(CurrentPassword, NewPassword, NewPasswordConfirm, ct);
            PasswordSuccess = _localizer["Account.Password.Changed"];
            CurrentPassword = string.Empty;
            NewPassword = string.Empty;
            NewPasswordConfirm = string.Empty;
        }
        catch (ApiException ex)
        {
            Passwsalesror = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while changing password");
            Passwsalesror = string.Format(_localizer["Account.Password.ChangeFailed"], ex.Message);
        }
        finally
        {
            IsChangingPassword = false;
        }
    }

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        switch (propertyName)
        {
            case nameof(Email):
            case nameof(Firstname):
            case nameof(Lastname):
                base.OnPropertyChanged(nameof(CanSaveProfile));
                break;
            case nameof(CurrentPassword):
            case nameof(NewPassword):
            case nameof(NewPasswordConfirm):
                base.OnPropertyChanged(nameof(CanChangePassword));
                break;
            case nameof(ProfileError):
                base.OnPropertyChanged(nameof(HasProfileError));
                break;
            case nameof(ProfileSuccess):
                base.OnPropertyChanged(nameof(HasProfileSuccess));
                break;
            case nameof(Passwsalesror):
                base.OnPropertyChanged(nameof(HasPasswsalesror));
                break;
            case nameof(PasswordSuccess):
                base.OnPropertyChanged(nameof(HasPasswordSuccess));
                break;
        }
    }
}
