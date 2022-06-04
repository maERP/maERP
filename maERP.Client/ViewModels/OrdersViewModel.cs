using System.Collections.ObjectModel;
using maERP.Client.Contracts;
using maERP.Data.Dtos.Order;
using maERP.Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace maERP.Client.ViewModels;

public partial class OrdersViewModel : BaseViewModel
{
    public ObservableCollection<OrderDto> Orders { get; } = new();
    IDataService<ICollection<OrderDto>> _dataService;

    public OrdersViewModel(IDataService<ICollection<OrderDto>> dataService)
    {
        Title = "Bestellungen";
        this._dataService = dataService;
    }

    [ICommand]
    async Task GetOrdersAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var orders = await _dataService.Request("GET", "/Products/getAll");

            if (Orders.Count != 0)
                Orders.Clear();

            foreach (var order in orders)
                Orders.Add(order);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unable to get monkeys: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}