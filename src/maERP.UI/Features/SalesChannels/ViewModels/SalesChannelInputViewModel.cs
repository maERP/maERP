using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Enums;
using maERP.Domain.Interfaces;
using maERP.UI.Features.SalesChannels.Validators;
using maERP.UI.Services;
using maERP.UI.Shared.Validation;

namespace maERP.UI.Features.SalesChannels.ViewModels;

public partial class SalesChannelInputViewModel : FluentValidationViewModelBase, ISalesChannelInputModel
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;
    private readonly SalesChannelClientValidator _validator;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private Guid salesChannelId;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private SalesChannelType salesChannelType = SalesChannelType.PointOfSale;

    [ObservableProperty]
    private string url = string.Empty;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private bool importProducts;

    [ObservableProperty]
    private bool exportProducts;

    [ObservableProperty]
    private bool importCustomers;

    [ObservableProperty]
    private bool exportCustomers;

    [ObservableProperty]
    private bool importOrders;

    [ObservableProperty]
    private bool exportOrders;

    [ObservableProperty]
    private ObservableCollection<WarehouseDetailDto> selectedWarehouses = new();

    [ObservableProperty]
    private WarehouseDetailDto? selectedWarehouse;

    [ObservableProperty]
    private ObservableCollection<WarehouseDetailDto> availableWarehouses = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    public bool IsEditMode => SalesChannelId != Guid.Empty;
    public string PageTitle => IsEditMode ? $"Vertriebskanal #{SalesChannelId} bearbeiten" : "Neuen Vertriebskanal erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    // Validation Error Properties for XAML Binding
    public string? NameError => GetFirstErrorMessage(nameof(Name));

    // Interface implementation - WarehouseIds as computed property from SelectedWarehouses
    public List<Guid> WarehouseIds => SelectedWarehouses.Select(w => w.Id).ToList();

    // Available options for dropdowns
    public List<SalesChannelType> AvailableSalesChannelTypes { get; } = Enum.GetValues<SalesChannelType>().ToList();

    public Action? GoBackAction { get; set; }
    public Func<Guid, Task>? NavigateToSalesChannelDetail { get; set; }

    public SalesChannelInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
        _validator = new SalesChannelClientValidator();
    }

    /// <summary>
    /// Gibt den FluentValidator für dieses ViewModel zurück.
    /// </summary>
    protected override IValidator GetValidator() => _validator;

    // Property-Change Validierung für Echtzeit-Feedback
    partial void OnNameChanged(string value) => ValidateProperty(nameof(Name));

    public async Task InitializeAsync(Guid salesChannelId = default)
    {
        SalesChannelId = salesChannelId;

        await LoadWarehousesAsync();

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
    private async Task LoadWarehousesAsync()
    {
        try
        {
            var result = await _httpService.GetPaginatedAsync<WarehouseDetailDto>("warehouses", 0, 1000, string.Empty, "Name Ascending");

            if (result != null && result.Succeeded && result.Data != null)
            {
                AvailableWarehouses.Clear();
                foreach (var warehouse in result.Data)
                {
                    AvailableWarehouses.Add(warehouse);
                }

                // Set default warehouse if creating new sales channel
                if (!IsEditMode && AvailableWarehouses.Any())
                {
                    SelectedWarehouse = AvailableWarehouses.First();
                }
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Exception loading warehouses");
        }
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (SalesChannelId == Guid.Empty) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<SalesChannelDetailDto>($"saleschannels/{SalesChannelId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var salesChannel = result.Data;

                // Map sales channel data to form fields
                Name = salesChannel.Name;
                SalesChannelType = salesChannel.SalesChannelType;
                Url = salesChannel.Url;
                Username = salesChannel.Username;
                Password = salesChannel.Password;
                ImportProducts = salesChannel.ImportProducts;
                ExportProducts = salesChannel.ExportProducts;
                ImportCustomers = salesChannel.ImportCustomers;
                ExportCustomers = salesChannel.ExportCustomers;
                ImportOrders = salesChannel.ImportOrders;
                ExportOrders = salesChannel.ExportOrders;
                // Set selected warehouses
                SelectedWarehouses.Clear();
                foreach (var warehouse in salesChannel.Warehouses)
                {
                    var availableWarehouse = AvailableWarehouses.FirstOrDefault(w => w.Id == warehouse.Id);
                    if (availableWarehouse != null)
                    {
                        SelectedWarehouses.Add(availableWarehouse);
                    }
                }

                _debugService.LogInfo($"Loaded sales channel {SalesChannelId} for editing successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Vertriebskanals {SalesChannelId}";
                _debugService.LogError($"Failed to load sales channel {SalesChannelId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Vertriebskanals: {ex.Message}";
            _debugService.LogError(ex, $"Exception loading sales channel {SalesChannelId}");
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
            var salesChannelDto = new SalesChannelInputDto
            {
                Id = SalesChannelId,
                Name = Name,
                SalesChannelType = SalesChannelType,
                Url = Url,
                Username = Username,
                Password = Password,
                ImportProducts = ImportProducts,
                ExportProducts = ExportProducts,
                ImportCustomers = ImportCustomers,
                ExportCustomers = ExportCustomers,
                ImportOrders = ImportOrders,
                ExportOrders = ExportOrders,
                WarehouseIds = SelectedWarehouses.Select(w => w.Id).ToList()
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<SalesChannelInputDto, Guid>($"saleschannels/{SalesChannelId}", salesChannelDto)
                : await _httpService.PostAsync<SalesChannelInputDto, Guid>("saleschannels", salesChannelDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"Sales channel {(IsEditMode ? "updated" : "created")} successfully");

                if (IsEditMode && NavigateToSalesChannelDetail != null)
                {
                    await NavigateToSalesChannelDetail(SalesChannelId);
                }
                else if (!IsEditMode && result.Data != Guid.Empty && NavigateToSalesChannelDetail != null)
                {
                    await NavigateToSalesChannelDetail(result.Data);
                }
                else
                {
                    GoBack();
                }
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim {(IsEditMode ? "Speichern" : "Erstellen")} des Vertriebskanals";
                _debugService.LogError($"Failed to save sales channel: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            _debugService.LogError(ex, "Exception saving sales channel");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        GoBack();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private void OnWarehouseSelectionChanged(WarehouseDetailDto? warehouse)
    {
        if (warehouse != null)
        {
            SelectedWarehouse = warehouse;
            if (!SelectedWarehouses.Contains(warehouse))
            {
                SelectedWarehouses.Add(warehouse);
            }
        }
    }

    [RelayCommand]
    private void RemoveWarehouse(WarehouseDetailDto warehouse)
    {
        SelectedWarehouses.Remove(warehouse);
    }

    private void ClearForm()
    {
        Name = string.Empty;
        SalesChannelType = SalesChannelType.PointOfSale;
        Url = string.Empty;
        Username = string.Empty;
        Password = string.Empty;
        ImportProducts = false;
        ExportProducts = false;
        ImportCustomers = false;
        ExportCustomers = false;
        ImportOrders = false;
        ExportOrders = false;
        SelectedWarehouses.Clear();
        SelectedWarehouse = null;
        ErrorMessage = string.Empty;

        ClearErrors();
    }

    private bool ValidateForm()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Eingabefehler";
            return false;
        }

        if (!SelectedWarehouses.Any())
        {
            ErrorMessage = "Bitte wählen Sie mindestens ein Lager aus";
            return false;
        }

        return true;
    }
}