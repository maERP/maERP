using maERP.Client.Features.Warehouses.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Warehouses.Views;

public sealed partial class WarehouseDetailPage : Page
{
    public WarehouseDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is WarehouseDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is WarehouseDetailModel model)
        {
            await model.EditWarehouse();
        }
    }
}
