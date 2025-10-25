using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Dashboard.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private string welcomeMessage = "Willkommen bei maERP";

    [ObservableProperty]
    private DateTime currentDate = DateTime.Now;

    public Func<Task>? NavigateToCreateOrder { get; set; }
    public Func<Task>? NavigateToCreateCustomer { get; set; }
    public Func<string, Task>? NavigateToMenuItem { get; set; }

    public DashboardViewModel()
    {
        // Hier können später Dashboard-spezifische Daten geladen werden
    }

    [RelayCommand]
    private async Task CreateOrder()
    {
        if (NavigateToCreateOrder != null)
            await NavigateToCreateOrder();
    }

    [RelayCommand]
    private async Task CreateCustomer()
    {
        if (NavigateToCreateCustomer != null)
            await NavigateToCreateCustomer();
    }

    [RelayCommand]
    private async Task ManageInvoices()
    {
        if (NavigateToMenuItem != null)
            await NavigateToMenuItem("Invoices");
    }

    [RelayCommand]
    private async Task ManageWarehouses()
    {
        if (NavigateToMenuItem != null)
            await NavigateToMenuItem("Warehouses");
    }
}