using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Client.Features.Shell.Models;
using maERP.Client.Features.Warehouses.Services;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.SalesChannels.Models;

/// <summary>
/// Navigation data for SalesChannelEditModel.
/// </summary>
public record SalesChannelEditData(Guid? SalesChannelId = null);

/// <summary>
/// Model for sales channel edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class SalesChannelEditModel : AsyncInitializableModel
{
    private readonly ISalesChannelService _salesChannelService;
    private readonly IWarehouseService _warehouseService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _salesChannelId;

    // Basic Information
    private string _name = string.Empty;
    private SalesChannelType _salesChannelType = SalesChannelType.Shopware6;

    // Connection Information
    private string _url = string.Empty;
    private string _username = string.Empty;
    private string _password = string.Empty;

    // Import Settings
    private bool _importProducts;
    private bool _importCustomers;
    private bool _importOrders;

    // Export Settings
    private bool _exportProducts;
    private bool _exportCustomers;
    private bool _exportOrders;

    // Initial Status
    private bool _initialProductImportCompleted;
    private bool _initialProductExportCompleted;

    // Warehouses
    private ObservableCollection<SelectableWarehouse> _warehouses = new();

    // OAuth state — populated for eBay / Amazon channels after the channel has been saved.
    private bool _hasRefreshToken;
    private DateTime? _tokenExpiresAt;
    private string _oAuthStatusMessage = string.Empty;
    private bool _isConnecting;

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public SalesChannelEditModel(
        ISalesChannelService salesChannelService,
        IWarehouseService warehouseService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<SalesChannelEditModel> logger,
        SalesChannelEditData? data = null)
        : base(logger)
    {
        _salesChannelService = salesChannelService;
        _warehouseService = warehouseService;
        _navigator = navigator;
        _localizer = localizer;
        _salesChannelId = data?.SalesChannelId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        await LoadWarehousesAsync(ct);

        if (_salesChannelId.HasValue)
        {
            await LoadSalesChannelAsync(ct);
        }
    }

    private async Task LoadWarehousesAsync(CancellationToken ct)
    {
        var parameters = new Core.Models.QueryParameters { PageSize = 1000 };
        var response = await _warehouseService.GetWarehousesAsync(parameters, ct);

        Warehouses.Clear();
        foreach (var warehouse in response.Data)
        {
            Warehouses.Add(new SelectableWarehouse
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                IsSelected = false
            });
        }
    }

    public bool IsEditMode => _salesChannelId.HasValue;

    public string Title => IsEditMode
        ? _localizer["SalesChannelEditPage.TitleEdit"]
        : _localizer["SalesChannelEditPage.TitleNew"];

    /// <summary>
    /// Available sales channel type options for the ComboBox.
    /// </summary>
    public IReadOnlyList<SalesChannelTypeOption> SalesChannelTypeOptions { get; } = new List<SalesChannelTypeOption>
    {
        new(SalesChannelType.PointOfSale, "SalesChannelType.PointOfSale"),
        new(SalesChannelType.Shopware5, "SalesChannelType.Shopware5"),
        new(SalesChannelType.Shopware6, "SalesChannelType.Shopware6"),
        new(SalesChannelType.WooCommerce, "SalesChannelType.WooCommerce"),
        new(SalesChannelType.eBay, "SalesChannelType.eBay"),
        new(SalesChannelType.Amazon, "SalesChannelType.Amazon")
    };

    #region Basic Information

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public SalesChannelType SalesChannelType
    {
        get => _salesChannelType;
        set
        {
            if (SetProperty(ref _salesChannelType, value))
            {
                // Update visibility properties when type changes
                OnPropertyChanged(nameof(IsOAuthChannel));
                OnPropertyChanged(nameof(ShowConnectionInfo));
                OnPropertyChanged(nameof(ShowConnectionHint));
                OnPropertyChanged(nameof(ShowOAuthSection));
                OnPropertyChanged(nameof(ShowOAuthSaveFirstHint));
                OnPropertyChanged(nameof(ConnectionStatusLabel));
                OnPropertyChanged(nameof(ShowUrlField));
                OnPropertyChanged(nameof(ShowImportExportSettings));
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    #endregion

    #region Type-Specific Visibility

    /// <summary>
    /// True for OAuth Authorization-Code channels (eBay, Amazon). These hide the
    /// Username/Password inputs in favor of a "Connect" button that triggers the OAuth flow.
    /// </summary>
    public bool IsOAuthChannel => SalesChannelType is SalesChannelType.eBay or SalesChannelType.Amazon;

    /// <summary>
    /// Shows the Username/Password connection block for credential-style channels (Shopware5/6,
    /// WooCommerce). PointOfSale skips it entirely; OAuth channels use <see cref="ShowOAuthSection"/>.
    /// </summary>
    public bool ShowConnectionInfo =>
        SalesChannelType != SalesChannelType.PointOfSale && !IsOAuthChannel;

    /// <summary>OAuth section is only useful once the channel has been persisted (we need its id).</summary>
    public bool ShowOAuthSection => IsOAuthChannel && IsEditMode;

    /// <summary>Hint shown on the OAuth section when the channel has never been saved.</summary>
    public bool ShowOAuthSaveFirstHint => IsOAuthChannel && !IsEditMode;

    public bool IsConnected => _hasRefreshToken;

    public string ConnectionStatusLabel
    {
        get
        {
            if (!IsOAuthChannel) return string.Empty;
            if (!_hasRefreshToken) return _localizer["SalesChannelEditPage.OAuth.StatusNotConnected"];
            var expiry = _tokenExpiresAt is null
                ? string.Empty
                : $" — {_tokenExpiresAt.Value.ToLocalTime():g}";
            return _localizer["SalesChannelEditPage.OAuth.StatusConnected"] + expiry;
        }
    }

    public string OAuthStatusMessage
    {
        get => _oAuthStatusMessage;
        private set => SetProperty(ref _oAuthStatusMessage, value);
    }

    public bool IsConnecting
    {
        get => _isConnecting;
        private set
        {
            if (SetProperty(ref _isConnecting, value))
            {
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoading));
                OnPropertyChanged(nameof(ShowConnectionHint));
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    /// <summary>
    /// Shows URL field only for Shopware5, Shopware6, and WooCommerce.
    /// eBay, Amazon and PointOfSale do not require URL.
    /// </summary>
    public bool ShowUrlField => SalesChannelType is
        SalesChannelType.Shopware5 or
        SalesChannelType.Shopware6 or
        SalesChannelType.WooCommerce;

    /// <summary>
    /// Shows import/export settings for all types except PointOfSale.
    /// </summary>
    public bool ShowImportExportSettings => SalesChannelType != SalesChannelType.PointOfSale;

    /// <summary>
    /// Shows the bottom-of-page hint that explains URL/credentials configuration.
    /// Only meaningful for credential-style channels (Shopware5/6, WooCommerce). Hidden for
    /// PointOfSale (no remote endpoint) and OAuth channels (eBay/Amazon — no URL/credentials).
    /// </summary>
    public bool ShowConnectionHint => IsNotLoading && ShowConnectionInfo;

    #endregion

    #region Connection Information

    public string Url
    {
        get => _url;
        set => SetProperty(ref _url, value);
    }

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    #endregion

    #region Import Settings

    public bool ImportProducts
    {
        get => _importProducts;
        set => SetProperty(ref _importProducts, value);
    }

    public bool ImportCustomers
    {
        get => _importCustomers;
        set => SetProperty(ref _importCustomers, value);
    }

    public bool ImportOrders
    {
        get => _importOrders;
        set => SetProperty(ref _importOrders, value);
    }

    #endregion

    #region Export Settings

    public bool ExportProducts
    {
        get => _exportProducts;
        set => SetProperty(ref _exportProducts, value);
    }

    public bool ExportCustomers
    {
        get => _exportCustomers;
        set => SetProperty(ref _exportCustomers, value);
    }

    public bool ExportOrders
    {
        get => _exportOrders;
        set => SetProperty(ref _exportOrders, value);
    }

    #endregion

    #region Initial Status

    public bool InitialProductImportCompleted
    {
        get => _initialProductImportCompleted;
        set => SetProperty(ref _initialProductImportCompleted, value);
    }

    public bool InitialProductExportCompleted
    {
        get => _initialProductExportCompleted;
        set => SetProperty(ref _initialProductExportCompleted, value);
    }

    #endregion

    #region Warehouses

    public ObservableCollection<SelectableWarehouse> Warehouses
    {
        get => _warehouses;
        set => SetProperty(ref _warehouses, value);
    }

    public bool HasWarehouses => Warehouses.Count > 0;

    #endregion

    #region UI State

    /// <summary>
    /// Indicates whether a save operation is in progress.
    /// </summary>
    public bool IsSaving
    {
        get => _isSaving;
        private set
        {
            if (SetProperty(ref _isSaving, value))
            {
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoading));
                OnPropertyChanged(nameof(ShowConnectionHint));
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    /// <summary>
    /// Combined loading state (initializing or saving).
    /// </summary>
    public bool IsLoading => IsInitializing || IsSaving;

    /// <summary>
    /// Inverse of IsLoading for binding convenience.
    /// </summary>
    public bool IsNotLoading => !IsLoading;

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    /// <summary>
    /// Determines if the save operation is allowed based on required fields per SalesChannelType.
    /// </summary>
    public bool CanSave
    {
        get
        {
            if (IsLoading || string.IsNullOrWhiteSpace(Name))
                return false;

            // Type-specific validation
            return SalesChannelType switch
            {
                // PointOfSale: Only Name required
                SalesChannelType.PointOfSale => true,

                // eBay / Amazon: only Name required at save time. The OAuth flow runs after save
                // (it needs the channel id) and persists the refresh token onto the channel.
                // Developer-App credentials live in TenantOAuthAppSettings or system Settings.
                SalesChannelType.eBay or SalesChannelType.Amazon => true,

                // Shopware5, Shopware6, WooCommerce: Name, URL, Username, Password required
                _ => !string.IsNullOrWhiteSpace(Url) &&
                     !string.IsNullOrWhiteSpace(Username) &&
                     !string.IsNullOrWhiteSpace(Password)
            };
        }
    }

    #endregion

    private async Task LoadSalesChannelAsync(CancellationToken ct)
    {
        if (!_salesChannelId.HasValue) return;

        var salesChannel = await _salesChannelService.GetSalesChannelAsync(_salesChannelId.Value, ct);
        if (salesChannel != null)
        {
            // Basic Information
            Name = salesChannel.Name;
            SalesChannelType = salesChannel.SalesChannelType;

            // Connection Information
            Url = salesChannel.Url ?? string.Empty;
            Username = salesChannel.Username ?? string.Empty;
            // Password is not returned from API for security reasons, keep empty

            // Import Settings
            ImportProducts = salesChannel.ImportProducts;
            ImportCustomers = salesChannel.ImportCustomers;
            ImportOrders = salesChannel.ImportOrders;

            // Export Settings
            ExportProducts = salesChannel.ExportProducts;
            ExportCustomers = salesChannel.ExportCustomers;
            ExportOrders = salesChannel.ExportOrders;

            // Note: InitialProductImportCompleted and InitialProductExportCompleted
            // are not returned by the API in DetailDto, so we keep them as default (false).
            // They are only used for initial setup when creating/updating a sales channel.

            // OAuth status (only meaningful for eBay / Amazon channels).
            _hasRefreshToken = salesChannel.HasRefreshToken;
            _tokenExpiresAt = salesChannel.TokenExpiresAt;
            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(ConnectionStatusLabel));

            // Mark associated warehouses as selected
            var warehouseIds = salesChannel.Warehouses?.Select(w => w.Id).ToHashSet() ?? new HashSet<Guid>();
            foreach (var warehouse in Warehouses)
            {
                warehouse.IsSelected = warehouseIds.Contains(warehouse.Id);
            }
        }
    }

    /// <summary>
    /// Begin OAuth flow: ask the Server for the authorize URL and open it in the system browser.
    /// Then poll the channel detail every 3 seconds for the connected state until either
    /// connected, the user navigates away, or 5 minutes elapse.
    /// </summary>
    public async Task ConnectOAuthAsync(CancellationToken ct = default)
    {
        if (!_salesChannelId.HasValue || !IsOAuthChannel) return;

        IsConnecting = true;
        OAuthStatusMessage = string.Empty;
        try
        {
            var providerSlug = SalesChannelType.ToString().ToLowerInvariant();
            var startResult = await _salesChannelService.StartOAuthAsync(_salesChannelId.Value, providerSlug, ct);

            await Windows.System.Launcher.LaunchUriAsync(new Uri(startResult.AuthorizeUrl));
            OAuthStatusMessage = _localizer["SalesChannelEditPage.OAuth.WaitingForCallback"];

            await PollForConnectionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(3), ct);
        }
        catch (ApiException ex)
        {
            OAuthStatusMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            OAuthStatusMessage = ex.Message;
        }
        finally
        {
            IsConnecting = false;
        }
    }

    public async Task DisconnectOAuthAsync(CancellationToken ct = default)
    {
        if (!_salesChannelId.HasValue || !IsOAuthChannel) return;

        IsConnecting = true;
        try
        {
            var providerSlug = SalesChannelType.ToString().ToLowerInvariant();
            await _salesChannelService.DisconnectOAuthAsync(_salesChannelId.Value, providerSlug, ct);

            _hasRefreshToken = false;
            _tokenExpiresAt = null;
            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(ConnectionStatusLabel));
            OAuthStatusMessage = _localizer["SalesChannelEditPage.OAuth.Disconnected"];
        }
        catch (ApiException ex)
        {
            OAuthStatusMessage = ex.CombinedMessage;
        }
        finally
        {
            IsConnecting = false;
        }
    }

    private async Task PollForConnectionAsync(TimeSpan timeout, TimeSpan interval, CancellationToken ct)
    {
        if (!_salesChannelId.HasValue) return;

        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            try
            {
                await Task.Delay(interval, ct);
            }
            catch (TaskCanceledException) { return; }

            try
            {
                var refreshed = await _salesChannelService.GetSalesChannelAsync(_salesChannelId.Value, ct);
                if (refreshed?.HasRefreshToken == true)
                {
                    _hasRefreshToken = true;
                    _tokenExpiresAt = refreshed.TokenExpiresAt;
                    OnPropertyChanged(nameof(IsConnected));
                    OnPropertyChanged(nameof(ConnectionStatusLabel));
                    OAuthStatusMessage = _localizer["SalesChannelEditPage.OAuth.Connected"];
                    return;
                }
            }
            catch
            {
                // transient — keep polling
            }
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new SalesChannelInputDto
            {
                Name = Name,
                SalesChannelType = SalesChannelType,
                Url = Url,
                Username = Username,
                Password = Password,
                ImportProducts = ImportProducts,
                ImportCustomers = ImportCustomers,
                ImportOrders = ImportOrders,
                ExportProducts = ExportProducts,
                ExportCustomers = ExportCustomers,
                ExportOrders = ExportOrders,
                InitialProductImportCompleted = InitialProductImportCompleted,
                InitialProductExportCompleted = InitialProductExportCompleted,
                WarehouseIds = Warehouses.Where(w => w.IsSelected).Select(w => w.Id).ToList()
            };

            if (_salesChannelId.HasValue)
            {
                input.Id = _salesChannelId.Value;
                await _salesChannelService.UpdateSalesChannelAsync(_salesChannelId.Value, input, ct);
            }
            else
            {
                await _salesChannelService.CreateSalesChannelAsync(input, ct);
            }

            // Notify Shell to refresh dynamic sidebar items
            ShellModel.NotifySalesChannelsChanged();

            await _navigator.NavigateBackAsync(this);
        }
        catch (ApiException ex)
        {
            // Display detailed error messages from the API
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["SalesChannelEditPage.Error.SaveFailed"], ex.Message);
        }
        finally
        {
            IsSaving = false;
        }
    }

    public async Task CancelAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        // Update CanSave when relevant fields change
        if (propertyName is nameof(Name) or nameof(Url) or nameof(Username) or nameof(Password))
        {
            base.OnPropertyChanged(nameof(CanSave));
        }

        // Handle IsInitializing changes from base class
        if (propertyName is nameof(IsInitializing))
        {
            base.OnPropertyChanged(nameof(IsLoading));
            base.OnPropertyChanged(nameof(IsNotLoading));
            base.OnPropertyChanged(nameof(ShowConnectionHint));
            base.OnPropertyChanged(nameof(CanSave));
        }
    }
}

/// <summary>
/// Represents a sales channel type option for the ComboBox.
/// </summary>
public record SalesChannelTypeOption(SalesChannelType Value, string ResourceKey);

/// <summary>
/// Selectable warehouse model for the sales channel edit form.
/// </summary>
public class SelectableWarehouse : INotifyPropertyChanged
{
    private Guid _id;
    private string _name = string.Empty;
    private bool _isSelected;

    public Guid Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
