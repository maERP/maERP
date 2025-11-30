using maERP.Client.Features.SalesChannels.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.SalesChannels.Views;

public sealed partial class SalesChannelDetailPage : Page
{
    public SalesChannelDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesChannelDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesChannelDetailModel model)
        {
            await model.EditSalesChannel();
        }
    }
}
