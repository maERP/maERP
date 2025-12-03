using maERP.Client.Features.SalesChannels.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.SalesChannels.Views;

public sealed partial class SalesChannelEditPage : Page
{
    public SalesChannelEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesChannelEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesChannelEditModel model)
        {
            await model.SaveAsync();
        }
    }

    private void SelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesChannelEditModel model)
        {
            foreach (var warehouse in model.Warehouses)
            {
                warehouse.IsSelected = true;
            }
        }
    }

    private void SelectAllCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesChannelEditModel model)
        {
            foreach (var warehouse in model.Warehouses)
            {
                warehouse.IsSelected = false;
            }
        }
    }
}
