using System.ComponentModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.SystemOAuthSettings.Services;
using maERP.Domain.Dtos.SystemOAuthSettings;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.SystemOAuthSettings.Models;

/// <summary>
/// Edits the system-wide <c>OAuth.{Provider}.*</c> Setting bundle for one provider at a time
/// (eBay or Amazon — switched via <see cref="SelectedProvider"/>). Loads on initialization and
/// after every provider switch; saves via the bundle controller. ClientSecret is write-only —
/// the loaded view never carries it; the user must explicitly enter a new one to rotate.
/// </summary>
public class SystemOAuthSettingsModel : AsyncInitializableModel
{
    private readonly ISystemOAuthSettingsService _service;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;

    private SalesChannelType _selectedProvider = SalesChannelType.eBay;
    private string _clientId = string.Empty;
    private string _clientSecret = string.Empty;
    private bool _hasClientSecret;
    private string _ruName = string.Empty;
    private string _redirectUri = string.Empty;
    private string _authorizationEndpoint = string.Empty;
    private string _tokenEndpoint = string.Empty;
    private string _scopes = string.Empty;
    private bool _useSandbox;
    private string _publicBaseUrl = string.Empty;

    private bool _isSaving;
    private string _statusMessage = string.Empty;
    private string _errorMessage = string.Empty;

    public SystemOAuthSettingsModel(
        ISystemOAuthSettingsService service,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<SystemOAuthSettingsModel> logger)
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

    /// <summary>Bool wrappers used by simple ComboBox bindings — XAML toggles a plain SelectedIndex.</summary>
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

    public string ClientId { get => _clientId; set => SetProperty(ref _clientId, value); }
    public string ClientSecret { get => _clientSecret; set => SetProperty(ref _clientSecret, value); }
    public bool HasClientSecret { get => _hasClientSecret; private set => SetProperty(ref _hasClientSecret, value); }
    public string ClientSecretPlaceholder => _hasClientSecret
        ? _localizer["SystemOAuthSettings.ClientSecretSetHint"]
        : string.Empty;

    public string RuName { get => _ruName; set => SetProperty(ref _ruName, value); }
    public string RedirectUri { get => _redirectUri; set => SetProperty(ref _redirectUri, value); }
    public string AuthorizationEndpoint { get => _authorizationEndpoint; set => SetProperty(ref _authorizationEndpoint, value); }
    public string TokenEndpoint { get => _tokenEndpoint; set => SetProperty(ref _tokenEndpoint, value); }
    public string Scopes { get => _scopes; set => SetProperty(ref _scopes, value); }
    public bool UseSandbox { get => _useSandbox; set => SetProperty(ref _useSandbox, value); }
    public string PublicBaseUrl { get => _publicBaseUrl; set => SetProperty(ref _publicBaseUrl, value); }

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
                new SystemOAuthSettingsInputDto
                {
                    ClientId = ClientId,
                    // Empty input means "do not rotate" — let it stay null on the wire.
                    ClientSecret = string.IsNullOrEmpty(ClientSecret) ? null : ClientSecret,
                    RuName = RuName,
                    RedirectUri = RedirectUri,
                    AuthorizationEndpoint = AuthorizationEndpoint,
                    TokenEndpoint = TokenEndpoint,
                    Scopes = Scopes,
                    UseSandbox = UseSandbox,
                    PublicBaseUrl = PublicBaseUrl,
                },
                ct);

            // Clear the secret field; reload to pick up HasClientSecret=true if one was just set.
            ClientSecret = string.Empty;
            await LoadAsync(ct);
            StatusMessage = _localizer["SystemOAuthSettings.SaveSucceeded"];
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

            ClientId = dto.ClientId ?? string.Empty;
            HasClientSecret = dto.HasClientSecret;
            RuName = dto.RuName ?? string.Empty;
            RedirectUri = dto.RedirectUri ?? string.Empty;
            AuthorizationEndpoint = dto.AuthorizationEndpoint ?? string.Empty;
            TokenEndpoint = dto.TokenEndpoint ?? string.Empty;
            Scopes = dto.Scopes ?? string.Empty;
            UseSandbox = dto.UseSandbox;
            PublicBaseUrl = dto.PublicBaseUrl ?? string.Empty;

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
