using maERP.Client.Features.AiModels.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.AiModels.Views;

public sealed partial class AiModelDetailPage : Page
{
    public AiModelDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AiModelDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AiModelDetailModel model)
        {
            await model.EditAiModel();
        }
    }
}
