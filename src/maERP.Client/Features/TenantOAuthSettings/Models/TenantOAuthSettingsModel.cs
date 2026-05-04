using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.TenantOAuthSettings.Services;
using maERP.Domain.Dtos.TenantOAuthAppSettings;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.TenantOAuthSettings.Models;

/// <summary>
/// Per-tenant override for OAuth Developer-App credentials. Mirrors <c>SystemOAuthSettingsModel</c>
/// in shape, but writes through <c>TenantOAuthAppSettingsController</c>. Empty fields fall back
/// to the system-level <c>Setting</c> rows at runtime — see <c>OAuthAppSettingsService</c>.
/// </summary>
public class TenantOAuthSettingsModel : AsyncInitializableModel
{
    private readonly ITenantOAuthSettingsService _service;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;

    private SalesChannelType _selectedProvider = SalesChannelType.eBay;
    private bool _isActive = true;
    private string _clientId = string.Empty;
    private string _clientSecret = string.Empty;
    private bool _hasClientSecret;
    private string _ruName = string.Empty;
    private string _redirectUri = string.Empty;
    private string _scopes = string.Empty;
    private bool? _useSandbox;

    private bool _isSaving;
    private string _statusMessage = string.Empty;
    private string _errorMessage = string.Empty;

    public TenantOAuthSettingsModel(
        ITenantOAuthSettingsService service,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<TenantOAuthSettingsModel> logger)
        : base(logger)
    {
        _service = service;
        _navigator = navigator;
        _localizer = localizer;
        StartInitialization();
    }

    protected override Task InitializeCoreAsync(CancellationToken ct) => LoadAsync(ct);

    public SalesChannelType SelectedProvider
    {
        get => _selectedProvider;
        set
        {
            if (SetProperty(ref _selectedProvider, value))
            {
                _ = LoadAsync(CancellationToken.None);
            }
        }
    }

    public bool IsEbaySelected
    {
        get => _selectedProvider == SalesChannelType.eBay;
        set { if (value) SelectedProvider = SalesChannelType.eBay; }
    }
    public bool IsAmazonSelected
    {
        get => _selectedProvider == SalesChannelType.Amazon;
        set { if (value) SelectedProvider = SalesChannelType.Amazon; }
    }

    public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }
    public string ClientId { get => _clientId; set => SetProperty(ref _clientId, value); }
    public string ClientSecret { get => _clientSecret; set => SetProperty(ref _clientSecret, value); }
    public bool HasClientSecret { get => _hasClientSecret; private set => SetProperty(ref _hasClientSecret, value); }
    public string ClientSecretPlaceholder => _hasClientSecret
        ? _localizer["TenantOAuthSettings.ClientSecretSetHint"]
        : _localizer["TenantOAuthSettings.ClientSecretFallbackHint"];

    public string RuName { get => _ruName; set => SetProperty(ref _ruName, value); }
    public string RedirectUri { get => _redirectUri; set => SetProperty(ref _redirectUri, value); }
    public string Scopes { get => _scopes; set => SetProperty(ref _scopes, value); }
    public bool? UseSandbox { get => _useSandbox; set => SetProperty(ref _useSandbox, value); }

    public bool IsSaving { get => _isSaving; private set => SetProperty(ref _isSaving, value); }
    public string StatusMessage { get => _statusMessage; private set => SetProperty(ref _statusMessage, value); }
    public string ErrorMessage { get => _errorMessage; private set => SetProperty(ref _errorMessage, value); }

    public bool IsRuNameRelevant => _selectedProvider == SalesChannelType.eBay;
    public bool IsRedirectUriRelevant => _selectedProvider == SalesChannelType.Amazon;

    public async Task SaveAsync(CancellationToken ct = default)
    {
        IsSaving = true;
        StatusMessage = string.Empty;
        ErrorMessage = string.Empty;
        try
        {
            await _service.UpsertAsync(
                _selectedProvider.ToString().ToLowerInvariant(),
                new TenantOAuthAppSettingsInputDto
                {
                    Provider = _selectedProvider,
                    IsActive = IsActive,
                    ClientId = string.IsNullOrEmpty(ClientId) ? null : ClientId,
                    ClientSecret = string.IsNullOrEmpty(ClientSecret) ? null : ClientSecret,
                    RuName = string.IsNullOrEmpty(RuName) ? null : RuName,
                    RedirectUri = string.IsNullOrEmpty(RedirectUri) ? null : RedirectUri,
                    Scopes = string.IsNullOrEmpty(Scopes) ? null : Scopes,
                    UseSandbox = UseSandbox,
                },
                ct);

            ClientSecret = string.Empty;
            await LoadAsync(ct);
            StatusMessage = _localizer["TenantOAuthSettings.SaveSucceeded"];
        }
        catch (ApiException ex)
        {
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsSaving = false;
        }
    }

    public async Task DeleteAsync(CancellationToken ct = default)
    {
        IsSaving = true;
        StatusMessage = string.Empty;
        ErrorMessage = string.Empty;
        try
        {
            await _service.DeleteAsync(_selectedProvider.ToString().ToLowerInvariant(), ct);
            await LoadAsync(ct);
            StatusMessage = _localizer["TenantOAuthSettings.DeleteSucceeded"];
        }
        catch (ApiException ex)
        {
            ErrorMessage = ex.CombinedMessage;
        }
        finally
        {
            IsSaving = false;
        }
    }

    public async Task GoBackAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    private async Task LoadAsync(CancellationToken ct)
    {
        try
        {
            var dto = await _service.GetAsync(_selectedProvider.ToString().ToLowerInvariant(), ct);
            if (dto is null) return;

            IsActive = dto.IsActive;
            ClientId = dto.ClientId ?? string.Empty;
            HasClientSecret = dto.HasClientSecret;
            RuName = dto.RuName ?? string.Empty;
            RedirectUri = dto.RedirectUri ?? string.Empty;
            Scopes = dto.Scopes ?? string.Empty;
            UseSandbox = dto.UseSandbox;

            OnPropertyChanged(nameof(IsRuNameRelevant));
            OnPropertyChanged(nameof(IsRedirectUriRelevant));
            OnPropertyChanged(nameof(ClientSecretPlaceholder));
        }
        catch (ApiException ex)
        {
            ErrorMessage = ex.CombinedMessage;
        }
    }

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(SelectedProvider))
        {
            base.OnPropertyChanged(nameof(IsEbaySelected));
            base.OnPropertyChanged(nameof(IsAmazonSelected));
            base.OnPropertyChanged(nameof(IsRuNameRelevant));
            base.OnPropertyChanged(nameof(IsRedirectUriRelevant));
        }
        if (propertyName == nameof(HasClientSecret))
        {
            base.OnPropertyChanged(nameof(ClientSecretPlaceholder));
        }
    }
}
