using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.AI.ViewModels;

namespace maERP.UI.Features.AI.Views;

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