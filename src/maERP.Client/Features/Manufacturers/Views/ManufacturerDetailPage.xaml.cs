using maERP.Client.Features.Manufacturers.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Manufacturers.Views;

public sealed partial class ManufacturerDetailPage : Page
{
    public ManufacturerDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ManufacturerDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ManufacturerDetailModel model)
        {
            await model.EditManufacturer();
        }
    }
}
