using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.ViewModels;

namespace maERP.UI.Views;

public partial class AiPromptListView : UserControl
{
    public AiPromptListView()
    {
        InitializeComponent();
    }
    
    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is AiPromptListViewModel viewModel && viewModel.SelectedAiPrompt != null)
        {
            viewModel.OpenAiPromptDetailCommand.Execute(viewModel.SelectedAiPrompt);
        }
    }
}