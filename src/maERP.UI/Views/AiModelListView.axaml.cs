using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.ViewModels;

namespace maERP.UI.Views;

public partial class AiModelListView : UserControl
{
    public AiModelListView()
    {
        InitializeComponent();
    }
    
    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is AiModelListViewModel viewModel && viewModel.SelectedAiModel != null)
        {
            viewModel.OpenAiModelDetailsCommand.Execute(viewModel.SelectedAiModel);
        }
    }
}