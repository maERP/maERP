using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.GoodsReceipt;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Dtos.Warehouse;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.GoodsReceipts.ViewModels;

public partial class GoodsReceiptInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private int goodsReceiptId;

    [ObservableProperty]
    private DateTime receiptDate = DateTime.Today;

    [ObservableProperty]
    [Required(ErrorMessage = "Produkt ist erforderlich")]
    [NotifyDataErrorInfo]
    private ProductListDto? selectedProduct;

    [ObservableProperty]
    [Required(ErrorMessage = "Menge ist erforderlich")]
    [Range(1, int.MaxValue, ErrorMessage = "Menge muss größer als 0 sein")]
    [NotifyDataErrorInfo]
    private int quantity = 1;

    [ObservableProperty]
    [Required(ErrorMessage = "Lager ist erforderlich")]
    [NotifyDataErrorInfo]
    private WarehouseListDto? selectedWarehouse;

    [ObservableProperty]
    private string supplier = string.Empty;

    [ObservableProperty]
    private string notes = string.Empty;

    [ObservableProperty]
    private ObservableCollection<ProductListDto> availableProducts = new();

    [ObservableProperty]
    private ObservableCollection<WarehouseListDto> availableWarehouses = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string successMessage = string.Empty;

    public bool ShouldShowContent => !IsLoading;
    public bool IsEditMode => GoodsReceiptId > 0;
    public string PageTitle => IsEditMode ? "Wareneingang bearbeiten" : "Neuer Wareneingang";

    public Func<Task>? GoBackAction { get; set; }

    public GoodsReceiptInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(int goodsReceiptId = 0)
    {
        GoodsReceiptId = goodsReceiptId;
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            // Load products and warehouses from API
            await LoadProductsAsync();
            await LoadWarehousesAsync();

            if (IsEditMode)
            {
                // Load existing goods receipt data
                await LoadExistingGoodsReceiptAsync();
            }

            _debugService.LogInfo($"Initialized GoodsReceiptInputViewModel (Edit mode: {IsEditMode})");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Daten: {ex.Message}";
            _debugService.LogError(ex, "Exception loading goods receipt input data");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveGoodsReceiptAsync()
    {
        if (!ValidateInput()) return;

        IsSaving = true;
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;

        try
        {
            var goodsReceiptDto = new GoodsReceiptInputDto
            {
                ReceiptDate = ReceiptDate,
                ProductId = SelectedProduct!.Id,
                Quantity = Quantity,
                WarehouseId = SelectedWarehouse!.Id,
                Supplier = Supplier,
                Notes = Notes
            };

            var result = await _httpService.PostAsync<GoodsReceiptInputDto, int>("goodsreceipts", goodsReceiptDto);

            if (result != null && result.Succeeded)
            {
                SuccessMessage = "Wareneingang erfolgreich gespeichert!";
                _debugService.LogInfo($"Successfully saved goods receipt with ID: {result.Data}");
                
                // Auto-clear success message after 3 seconds
                _ = Task.Delay(3000).ContinueWith(t => SuccessMessage = string.Empty);
            }
            else
            {
                ErrorMessage = result?.Messages?.FirstOrDefault() ?? "Fehler beim Speichern des Wareneingangs";
                _debugService.LogError($"Failed to save goods receipt: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            _debugService.LogError(ex, "Exception saving goods receipt");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private async Task SaveAndNewGoodsReceiptAsync()
    {
        if (!ValidateInput()) return;

        IsSaving = true;
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;

        try
        {
            var goodsReceiptDto = new GoodsReceiptInputDto
            {
                ReceiptDate = ReceiptDate,
                ProductId = SelectedProduct!.Id,
                Quantity = Quantity,
                WarehouseId = SelectedWarehouse!.Id,
                Supplier = Supplier,
                Notes = Notes
            };

            var result = await _httpService.PostAsync<GoodsReceiptInputDto, int>("goodsreceipts", goodsReceiptDto);

            if (result != null && result.Succeeded)
            {
                SuccessMessage = "Wareneingang gespeichert! Bereit für nächsten Eingang.";
                _debugService.LogInfo($"Successfully saved goods receipt with ID: {result.Data}");
                
                // Reset form for next entry
                ResetForm();
                
                // Auto-clear success message after 2 seconds
                _ = Task.Delay(2000).ContinueWith(t => SuccessMessage = string.Empty);
            }
            else
            {
                ErrorMessage = result?.Messages?.FirstOrDefault() ?? "Fehler beim Speichern des Wareneingangs";
                _debugService.LogError($"Failed to save goods receipt: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            _debugService.LogError(ex, "Exception saving goods receipt");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        if (GoBackAction != null)
        {
            await GoBackAction();
        }
    }

    private bool ValidateInput()
    {
        var isValid = true;
        ErrorMessage = string.Empty;

        if (SelectedProduct == null)
        {
            ErrorMessage = "Bitte wählen Sie ein Produkt aus.";
            isValid = false;
        }
        else if (SelectedWarehouse == null)
        {
            ErrorMessage = "Bitte wählen Sie ein Lager aus.";
            isValid = false;
        }
        else if (Quantity <= 0)
        {
            ErrorMessage = "Menge muss größer als 0 sein.";
            isValid = false;
        }

        return isValid;
    }

    private void ResetForm()
    {
        SelectedProduct = null;
        Quantity = 1;
        SelectedWarehouse = null;
        Supplier = string.Empty;
        Notes = string.Empty;
        ReceiptDate = DateTime.Today;
    }

    private async Task LoadProductsAsync()
    {
        try
        {
            var result = await _httpService.GetPaginatedAsync<ProductListDto>("products", 0, 1000, "", "Name Ascending");

            if (result != null && result.Succeeded && result.Data != null)
            {
                AvailableProducts.Clear();
                foreach (var product in result.Data)
                {
                    AvailableProducts.Add(product);
                }
                _debugService.LogInfo($"Loaded {AvailableProducts.Count} products for selection");
            }
            else
            {
                _debugService.LogWarning("Failed to load products for selection");
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Exception loading products for selection");
        }
    }

    private async Task LoadWarehousesAsync()
    {
        try
        {
            var result = await _httpService.GetPaginatedAsync<WarehouseListDto>("warehouses", 0, 1000, "", "Name Ascending");

            if (result != null && result.Succeeded && result.Data != null)
            {
                AvailableWarehouses.Clear();
                foreach (var warehouse in result.Data)
                {
                    AvailableWarehouses.Add(warehouse);
                }
                _debugService.LogInfo($"Loaded {AvailableWarehouses.Count} warehouses for selection");
            }
            else
            {
                _debugService.LogWarning("Failed to load warehouses for selection");
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Exception loading warehouses for selection");
        }
    }

    private async Task LoadExistingGoodsReceiptAsync()
    {
        try
        {
            var result = await _httpService.GetAsync<GoodsReceiptDetailDto>($"goodsreceipts/{GoodsReceiptId}");

            if (result != null && result.Succeeded && result.Data != null)
            {
                var goodsReceipt = result.Data;
                ReceiptDate = goodsReceipt.ReceiptDate;
                SelectedProduct = AvailableProducts.FirstOrDefault(p => p.Id == goodsReceipt.ProductId);
                Quantity = goodsReceipt.Quantity;
                SelectedWarehouse = AvailableWarehouses.FirstOrDefault(w => w.Id == goodsReceipt.WarehouseId);
                Supplier = goodsReceipt.Supplier;
                Notes = goodsReceipt.Notes;

                _debugService.LogInfo($"Loaded existing goods receipt for editing: ID {GoodsReceiptId}");
            }
            else
            {
                ErrorMessage = "Wareneingang konnte nicht geladen werden";
                _debugService.LogError($"Failed to load goods receipt for editing: ID {GoodsReceiptId}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Wareneingangs: {ex.Message}";
            _debugService.LogError(ex, $"Exception loading goods receipt for editing: ID {GoodsReceiptId}");
        }
    }
}