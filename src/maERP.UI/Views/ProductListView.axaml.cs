using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.ViewModels;

namespace maERP.UI.Views;

public partial class ProductListView : UserControl
{
    public ProductListView()
    {
        InitializeComponent();
    }
    
    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is ProductListViewModel viewModel && viewModel.SelectedProduct != null)
        {
            viewModel.ViewProductDetailsCommand.Execute(viewModel.SelectedProduct);
        }
    }
}