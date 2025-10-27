using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Shared.Features.AI.ViewModels;

namespace maERP.UI.Shared.Features.AI.Views;

public sealed partial class AiPromptListView : UserControl
{
    public AiPromptListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is AiPromptListViewModel viewModel && viewModel.SelectedAiPrompt != null)
        {
            viewModel.OpenAiPromptDetailCommand.Execute(viewModel.SelectedAiPrompt);
        }
    }
}
