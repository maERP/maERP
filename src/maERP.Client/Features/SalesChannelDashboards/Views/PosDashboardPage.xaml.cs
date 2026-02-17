using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.SalesChannelDashboards.Models;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.Product;
using Windows.ApplicationModel.Resources;
using Microsoft.UI.Xaml.Input;
using Windows.System;

namespace maERP.Client.Features.SalesChannelDashboards.Views;

public sealed partial class PosDashboardPage : Page
{
    private ItemsRepeater? _productResultsRepeater;
    private int _selectedProductIndex = -1;

    public PosDashboardPage()
    {
        this.InitializeComponent();
    }

    private async void CustomerSearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox && DataContext is PosDashboardModel model)
        {
            await model.CustomerSearchQuery.UpdateAsync(_ => textBox.Text);
        }
    }

    private async void ProductSearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        SetProductHighlight(-1);
        if (sender is TextBox textBox && DataContext is PosDashboardModel model)
        {
            await model.ProductSearchQuery.UpdateAsync(_ => textBox.Text);
        }
    }

    private void ProductResultsRepeater_Loaded(object sender, RoutedEventArgs e)
    {
        _productResultsRepeater = sender as ItemsRepeater;
        _selectedProductIndex = -1;
    }

    private void ProductSearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (_productResultsRepeater == null) return;

        switch (e.Key)
        {
            case VirtualKey.Down:
            {
                var nextIndex = _selectedProductIndex + 1;
                if (_productResultsRepeater.TryGetElement(nextIndex) != null)
                {
                    e.Handled = true;
                    SetProductHighlight(nextIndex);
                }
                break;
            }
            case VirtualKey.Up:
            {
                e.Handled = true;
                SetProductHighlight(Math.Max(_selectedProductIndex - 1, -1));
                break;
            }
            case VirtualKey.Enter when _selectedProductIndex >= 0:
            {
                e.Handled = true;
                var element = _productResultsRepeater.TryGetElement(_selectedProductIndex);
                if (element is FrameworkElement fe && fe.DataContext is ProductListDto product)
                {
                    _ = AddProductToCartAsync(product);
                }
                break;
            }
            case VirtualKey.Escape:
            {
                e.Handled = true;
                _ = ClearProductSearchAsync();
                break;
            }
        }
    }

    private void SetProductHighlight(int newIndex)
    {
        if (_selectedProductIndex >= 0 && _productResultsRepeater != null)
        {
            if (_productResultsRepeater.TryGetElement(_selectedProductIndex) is Border prevBorder)
            {
                prevBorder.Background = null;
            }
        }

        _selectedProductIndex = newIndex;

        if (_selectedProductIndex >= 0 && _productResultsRepeater != null)
        {
            if (_productResultsRepeater.TryGetElement(_selectedProductIndex) is Border currentBorder)
            {
                currentBorder.Background = Application.Current.Resources["SecondaryContainerBrush"] as Brush;
            }
        }
    }

    private async Task AddProductToCartAsync(ProductListDto product)
    {
        var model = this.DataContext as PosDashboardModel;
        if (model != null)
        {
            await model.AddToCart(product);
            SetProductHighlight(-1);
        }
    }

    private async Task ClearProductSearchAsync()
    {
        SetProductHighlight(-1);
        ProductSearchBox.Text = string.Empty;
        var model = this.DataContext as PosDashboardModel;
        if (model != null)
        {
            await model.ClearProductSearch();
        }
    }

    private async void OrderRow_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is RecentOrderItem order)
        {
            var model = this.DataContext as PosDashboardModel;
            if (model != null)
            {
                await model.ViewOrder(order);
            }
        }
    }

    private async void CustomerResult_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is CustomerListWithAddressDto customer)
        {
            var model = this.DataContext as PosDashboardModel;
            if (model != null)
            {
                await model.SelectCustomer(customer);
                ShowSelectedCustomer(customer);
            }
        }
    }

    private async void ClearCustomer_Click(object sender, RoutedEventArgs e)
    {
        var model = this.DataContext as PosDashboardModel;
        if (model != null)
        {
            await model.ClearCustomer();
        }

        SelectedCustomerPanel.Visibility = Visibility.Collapsed;
        CustomerSearchPanel.Visibility = Visibility.Visible;
        CustomerSearchBox.Text = string.Empty;
    }

    private void ShowSelectedCustomer(CustomerListWithAddressDto customer)
    {
        var resourceLoader = ResourceLoader.GetForViewIndependentUse();

        SelectedCustomerName.Text = customer.FullName;
        SelectedCustomerNumber.Text = $"{resourceLoader.GetString("PosDashboard.QuickSale.CustomerNumber.Text")} {customer.CustomerId}";
        SelectedCustomerEmail.Text = customer.Email;

        if (!string.IsNullOrWhiteSpace(customer.InvoiceAddress))
        {
            SelectedCustomerAddress.Text = customer.InvoiceAddress;
            SelectedCustomerAddress.Visibility = Visibility.Visible;
        }
        else
        {
            SelectedCustomerAddress.Text = resourceLoader.GetString("PosDashboard.QuickSale.NoAddress.Text");
            SelectedCustomerAddress.FontStyle = Windows.UI.Text.FontStyle.Italic;
            SelectedCustomerAddress.Visibility = Visibility.Visible;
        }

        SelectedCustomerPanel.Visibility = Visibility.Visible;
        CustomerSearchPanel.Visibility = Visibility.Collapsed;
        CustomerSearchBox.Text = string.Empty;
    }

    private async void AddToCart_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is ProductListDto product)
        {
            var model = this.DataContext as PosDashboardModel;
            if (model != null)
            {
                await model.AddToCart(product);
                await ClearProductSearchAsync();
            }
        }
    }

    private async void RemoveFromCart_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is PosCartItem cartItem)
        {
            var model = this.DataContext as PosDashboardModel;
            if (model != null)
            {
                await model.RemoveFromCart(cartItem);
            }
        }
    }

    private async void IncrementQuantity_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is PosCartItem cartItem)
        {
            var model = this.DataContext as PosDashboardModel;
            if (model != null)
            {
                await model.UpdateCartItemQuantity(cartItem, cartItem.Quantity + 1);
            }
        }
    }

    private async void DecrementQuantity_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is PosCartItem cartItem)
        {
            var model = this.DataContext as PosDashboardModel;
            if (model != null)
            {
                await model.UpdateCartItemQuantity(cartItem, cartItem.Quantity - 1);
            }
        }
    }

    private async void CartItemPrice_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && textBox.DataContext is PosCartItem cartItem)
        {
            var model = this.DataContext as PosDashboardModel;
            if (model != null && decimal.TryParse(textBox.Text, out var newPrice) && newPrice >= 0)
            {
                await model.UpdateCartItemPrice(cartItem, newPrice);
            }
            else
            {
                // Reset to original value on invalid input
                textBox.Text = cartItem.UnitPriceEditable;
            }
        }
    }

    private void InvoiceItemsRepeater_ElementPrepared(ItemsRepeater sender, ItemsRepeaterElementPreparedEventArgs args)
    {
        var element = args.Element;
        if (element is Grid grid)
        {
            var posTextBlock = grid.FindName("PosNumber") as TextBlock;
            if (posTextBlock != null)
            {
                posTextBlock.Text = (args.Index + 1).ToString();
            }
        }
    }
}
