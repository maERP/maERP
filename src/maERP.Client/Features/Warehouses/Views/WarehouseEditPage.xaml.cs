using maERP.Client.Features.Warehouses.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Warehouses.Views;

public sealed partial class WarehouseEditPage : Page
{
    public WarehouseEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is WarehouseEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is WarehouseEditModel model)
        {
            await model.SaveAsync();
        }
    }
}
