using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Shared.Features.AI.ViewModels;

namespace maERP.UI.Shared.Features.AI.Views;

public sealed partial class AiModelListView : UserControl
{
    public AiModelListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is AiModelListViewModel viewModel && viewModel.SelectedAiModel != null)
        {
            viewModel.OpenAiModelDetailsCommand.Execute(viewModel.SelectedAiModel);
        }
    }
}
