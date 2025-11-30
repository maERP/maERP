using maERP.Client.Features.TaxClasses.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.TaxClasses.Views;

public sealed partial class TaxClassEditPage : Page
{
    public TaxClassEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TaxClassEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TaxClassEditModel model)
        {
            await model.SaveAsync();
        }
    }

    private void QuickSelect_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is string tagValue &&
            double.TryParse(tagValue, out var taxRate) &&
            DataContext is TaxClassEditModel model)
        {
            model.TaxRate = taxRate;
        }
    }
}
