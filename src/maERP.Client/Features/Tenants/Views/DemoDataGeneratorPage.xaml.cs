using maERP.Client.Features.Tenants.Models;

namespace maERP.Client.Features.Tenants.Views;

public sealed partial class DemoDataGeneratorPage : Page
{
    public DemoDataGeneratorPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is DemoDataGeneratorModel model)
        {
            await model.NavigateBackAsync();
        }
    }

    private async void GenerateProducts_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is DemoDataGeneratorModel model)
        {
            await model.GenerateProductsAsync();
        }
    }

    private async void GenerateCustomers_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is DemoDataGeneratorModel model)
        {
            await model.GenerateCustomersAsync();
        }
    }

    private async void GenerateOrders_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is DemoDataGeneratorModel model)
        {
            await model.GenerateOrdersAsync();
        }
    }

    private async void GenerateAiData_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is DemoDataGeneratorModel model)
        {
            await model.GenerateAiDataAsync();
        }
    }
}
