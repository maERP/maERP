using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Features.Products.ViewModels;

namespace maERP.UI.Features.Products.Views;

public sealed partial class ProductListView : UserControl
{
    public ProductListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is ProductListViewModel viewModel && viewModel.SelectedProduct != null)
        {
            viewModel.ViewProductDetailsCommand.Execute(viewModel.SelectedProduct);
        }
    }
}
