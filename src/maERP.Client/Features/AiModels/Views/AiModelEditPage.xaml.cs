using maERP.Client.Features.AiModels.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.AiModels.Views;

public sealed partial class AiModelEditPage : Page
{
    public AiModelEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AiModelEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AiModelEditModel model)
        {
            await model.SaveAsync();
        }
    }
}
