using maERP.Client.Features.SalesChannels.Models;

namespace maERP.Client.Features.SalesChannels.Views;

public sealed partial class SalesChannelOverviewPage : Page
{
    public SalesChannelOverviewPage()
    {
        this.InitializeComponent();
    }

    private async void ChannelCard_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.DataContext is SalesChannelOverviewItem item &&
            DataContext is SalesChannelOverviewModel model)
        {
            await model.OpenChannel(item);
        }
    }

    private async void ManageList_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesChannelOverviewModel model)
        {
            await model.OpenChannelList();
        }
    }
}
