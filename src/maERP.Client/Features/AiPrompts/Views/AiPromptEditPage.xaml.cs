using maERP.Client.Features.AiPrompts.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.AiPrompts.Views;

public sealed partial class AiPromptEditPage : Page
{
    public AiPromptEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AiPromptEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AiPromptEditModel model)
        {
            await model.SaveAsync();
        }
    }
}
