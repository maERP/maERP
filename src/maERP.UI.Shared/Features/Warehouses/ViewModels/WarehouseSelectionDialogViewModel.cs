using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Warehouse;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Warehouses.ViewModels;

public partial class WarehouseSelectionDialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private List<WarehouseListDto> availableWarehouses = new();

    [ObservableProperty]
    private WarehouseListDto? selectedWarehouse;

    [ObservableProperty]
    private string title = "Lager auswählen";

    [ObservableProperty]
    private string message = "Wählen Sie ein Lager aus, dem die Produkte zugewiesen werden sollen:";

    public bool IsValidSelection => SelectedWarehouse != null;

    public Guid? SelectedWarehouseId => SelectedWarehouse?.Id;

    public void Initialize(List<WarehouseListDto> warehouses, string dialogTitle, string dialogMessage)
    {
        AvailableWarehouses = warehouses ?? new List<WarehouseListDto>();
        Title = dialogTitle;
        Message = dialogMessage;

        // Auto-select first warehouse if only one available
        if (AvailableWarehouses.Count == 1)
        {
            SelectedWarehouse = AvailableWarehouses.First();
        }

        OnPropertyChanged(nameof(IsValidSelection));
    }

    partial void OnSelectedWarehouseChanged(WarehouseListDto? value)
    {
        OnPropertyChanged(nameof(IsValidSelection));
        OnPropertyChanged(nameof(SelectedWarehouseId));
    }

    [RelayCommand]
    private void SelectWarehouse(WarehouseListDto warehouse)
    {
        SelectedWarehouse = warehouse;
    }
}