using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Warehouses.Services;
using maERP.Domain.Dtos.Warehouse;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Warehouses.Models;

/// <summary>
/// Model for warehouse edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class WarehouseEditModel : AsyncInitializableModel
{
    private readonly IWarehouseService _warehouseService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _warehouseId;

    // Warehouse Data
    private string _name = string.Empty;

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public WarehouseEditModel(
        IWarehouseService warehouseService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<WarehouseEditModel> logger,
        WarehouseEditData? data = null)
        : base(logger)
    {
        _warehouseService = warehouseService;
        _navigator = navigator;
        _localizer = localizer;
        _warehouseId = data?.WarehouseId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        if (_warehouseId.HasValue)
        {
            await LoadWarehouseAsync(ct);
        }
    }

    public bool IsEditMode => _warehouseId.HasValue;

    public string Title => IsEditMode
        ? _localizer["WarehouseEditPage.TitleEdit"]
        : _localizer["WarehouseEditPage.TitleNew"];

    #region Warehouse Data

    public string Name
    {
        get => _name;
        set
        {
            if (SetProperty(ref _name, value))
            {
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

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

    public bool CanSave => !string.IsNullOrWhiteSpace(Name) && !IsLoading;

    #endregion

    private async Task LoadWarehouseAsync(CancellationToken ct)
    {
        if (!_warehouseId.HasValue) return;

        var warehouse = await _warehouseService.GetWarehouseAsync(_warehouseId.Value, ct);
        if (warehouse != null)
        {
            Name = warehouse.Name;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new WarehouseInputDto
            {
                Name = Name
            };

            if (_warehouseId.HasValue)
            {
                input.Id = _warehouseId.Value;
                await _warehouseService.UpdateWarehouseAsync(_warehouseId.Value, input, ct);
            }
            else
            {
                await _warehouseService.CreateWarehouseAsync(input, ct);
            }

            await _navigator.NavigateBackAsync(this);
        }
        catch (ApiException ex)
        {
            // Display detailed error messages from the API
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["WarehouseEditPage.Error.SaveFailed"], ex.Message);
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

        // Handle IsInitializing changes from base class
        if (propertyName is nameof(IsInitializing))
        {
            base.OnPropertyChanged(nameof(IsLoading));
            base.OnPropertyChanged(nameof(IsNotLoading));
            base.OnPropertyChanged(nameof(CanSave));
        }
    }
}
