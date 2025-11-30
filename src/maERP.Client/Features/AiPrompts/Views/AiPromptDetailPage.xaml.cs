using maERP.Client.Features.AiPrompts.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.AiPrompts.Views;

public sealed partial class AiPromptDetailPage : Page
{
    public AiPromptDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AiPromptDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AiPromptDetailModel model)
        {
            await model.EditAiPrompt();
        }
    }
}
