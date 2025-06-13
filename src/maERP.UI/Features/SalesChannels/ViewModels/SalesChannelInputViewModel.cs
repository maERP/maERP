using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Enums;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.SalesChannels.ViewModels;

public partial class SalesChannelInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private int salesChannelId;

    [ObservableProperty]
    [Required(ErrorMessage = "Name ist erforderlich")]
    [NotifyDataErrorInfo]
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
    [Required(ErrorMessage = "Lager ist erforderlich")]
    [NotifyDataErrorInfo]
    private int warehouseId;

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

    public bool IsEditMode => SalesChannelId > 0;
    public string PageTitle => IsEditMode ? $"Vertriebskanal #{SalesChannelId} bearbeiten" : "Neuen Vertriebskanal erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    // Available options for dropdowns
    public List<SalesChannelType> AvailableSalesChannelTypes { get; } = Enum.GetValues<SalesChannelType>().ToList();

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToSalesChannelDetail { get; set; }

    public SalesChannelInputViewModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task InitializeAsync(int salesChannelId = 0)
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
                    WarehouseId = AvailableWarehouses.First().Id;
                    SelectedWarehouse = AvailableWarehouses.First();
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Exception loading warehouses: {ex}");
        }
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (SalesChannelId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<SalesChannelDetailDto>($"saleschannels/{SalesChannelId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                System.Diagnostics.Debug.WriteLine("GetAsync returned null - not authenticated or no server URL");
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
                WarehouseId = salesChannel.WarehouseId;
                
                // Set selected warehouse
                SelectedWarehouse = AvailableWarehouses.FirstOrDefault(w => w.Id == WarehouseId);

                System.Diagnostics.Debug.WriteLine($"Loaded sales channel {SalesChannelId} for editing successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Vertriebskanals {SalesChannelId}";
                System.Diagnostics.Debug.WriteLine($"Failed to load sales channel {SalesChannelId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Vertriebskanals: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception loading sales channel {SalesChannelId}: {ex}");
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
                WarehouseId = WarehouseId
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<SalesChannelInputDto, int>($"saleschannels/{SalesChannelId}", salesChannelDto)
                : await _httpService.PostAsync<SalesChannelInputDto, int>("saleschannels", salesChannelDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                System.Diagnostics.Debug.WriteLine("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                System.Diagnostics.Debug.WriteLine($"Sales channel {(IsEditMode ? "updated" : "created")} successfully");
                
                if (IsEditMode && NavigateToSalesChannelDetail != null)
                {
                    await NavigateToSalesChannelDetail(SalesChannelId);
                }
                else if (!IsEditMode && result.Data > 0 && NavigateToSalesChannelDetail != null)
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
                System.Diagnostics.Debug.WriteLine($"Failed to save sales channel: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception saving sales channel: {ex}");
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
            WarehouseId = warehouse.Id;
        }
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
        WarehouseId = AvailableWarehouses.Any() ? AvailableWarehouses.First().Id : 0;
        SelectedWarehouse = AvailableWarehouses.FirstOrDefault();
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

        if (WarehouseId <= 0)
        {
            ErrorMessage = "Bitte wählen Sie ein Lager aus";
            return false;
        }

        return true;
    }
}