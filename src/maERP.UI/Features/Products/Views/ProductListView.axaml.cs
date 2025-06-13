using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Products.ViewModels;

namespace maERP.UI.Features.Products.Views;

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