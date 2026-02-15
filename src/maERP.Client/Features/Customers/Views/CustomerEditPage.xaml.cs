using maERP.Client.Features.Customers.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Customers.Views;

public sealed partial class CustomerEditPage : Page
{
    public CustomerEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerEditModel model)
        {
            await model.SaveAsync();
        }
    }

    private async void AddAddressButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerEditModel model)
        {
            model.StartAddAddress();
            await ShowAddressDialogAsync(model);
        }
    }

    private async void EditAddressButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is EditableCustomerAddress address &&
            DataContext is CustomerEditModel model)
        {
            model.StartEditAddress(address);
            await ShowAddressDialogAsync(model);
        }
    }

    private void DeleteAddressButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is EditableCustomerAddress address &&
            DataContext is CustomerEditModel model)
        {
            model.DeleteAddress(address);
        }
    }

    private async Task ShowAddressDialogAsync(CustomerEditModel model)
    {
        if (model.EditingAddress == null) return;

        var dialog = CreateAddressDialog(model, model.EditingAddress);
        dialog.XamlRoot = this.XamlRoot;

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            model.SaveAddress();
        }
        else
        {
            model.CloseAddressDialog();
        }
    }

    private ContentDialog CreateAddressDialog(CustomerEditModel model, EditableCustomerAddress address)
    {
        var panel = new StackPanel { Spacing = 16 };

        var firstnameBox = new TextBox
        {
            Header = model.AddressFieldFirstname,
            PlaceholderText = model.AddressFieldFirstnamePlaceholder,
            Text = address.Firstname
        };
        firstnameBox.TextChanged += (s, e) => address.Firstname = firstnameBox.Text;

        var lastnameBox = new TextBox
        {
            Header = model.AddressFieldLastname,
            PlaceholderText = model.AddressFieldLastnamePlaceholder,
            Text = address.Lastname
        };
        lastnameBox.TextChanged += (s, e) => address.Lastname = lastnameBox.Text;

        var companyBox = new TextBox
        {
            Header = model.AddressFieldCompanyName,
            PlaceholderText = model.AddressFieldCompanyNamePlaceholder,
            Text = address.CompanyName
        };
        companyBox.TextChanged += (s, e) => address.CompanyName = companyBox.Text;

        var streetPanel = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(8, GridUnitType.Pixel) },
                new ColumnDefinition { Width = new GridLength(100, GridUnitType.Pixel) }
            }
        };

        var streetBox = new TextBox
        {
            Header = model.AddressFieldStreet,
            PlaceholderText = model.AddressFieldStreetPlaceholder,
            Text = address.Street
        };
        streetBox.TextChanged += (s, e) => address.Street = streetBox.Text;
        Grid.SetColumn(streetBox, 0);

        var houseNrBox = new TextBox
        {
            Header = model.AddressFieldHouseNr,
            PlaceholderText = model.AddressFieldHouseNrPlaceholder,
            Text = address.HouseNr
        };
        houseNrBox.TextChanged += (s, e) => address.HouseNr = houseNrBox.Text;
        Grid.SetColumn(houseNrBox, 2);

        streetPanel.Children.Add(streetBox);
        streetPanel.Children.Add(houseNrBox);

        var cityPanel = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(120, GridUnitType.Pixel) },
                new ColumnDefinition { Width = new GridLength(8, GridUnitType.Pixel) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
        };

        var zipBox = new TextBox
        {
            Header = model.AddressFieldZip,
            PlaceholderText = model.AddressFieldZipPlaceholder,
            Text = address.Zip
        };
        zipBox.TextChanged += (s, e) => address.Zip = zipBox.Text;
        Grid.SetColumn(zipBox, 0);

        var cityBox = new TextBox
        {
            Header = model.AddressFieldCity,
            PlaceholderText = model.AddressFieldCityPlaceholder,
            Text = address.City
        };
        cityBox.TextChanged += (s, e) => address.City = cityBox.Text;
        Grid.SetColumn(cityBox, 2);

        cityPanel.Children.Add(zipBox);
        cityPanel.Children.Add(cityBox);

        // Country ComboBox
        var countryComboBox = new ComboBox
        {
            Header = model.AddressFieldCountry,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            ItemsSource = model.Countries,
            DisplayMemberPath = "Name",
            SelectedValuePath = "Id"
        };

        // Set the selected country based on CountryId
        if (address.CountryId != Guid.Empty)
        {
            foreach (var country in model.Countries)
            {
                if (country.Id == address.CountryId)
                {
                    countryComboBox.SelectedItem = country;
                    break;
                }
            }
        }

        countryComboBox.SelectionChanged += (s, e) =>
        {
            if (countryComboBox.SelectedItem is Domain.Dtos.Country.CountryListDto selectedCountry)
            {
                address.CountryId = selectedCountry.Id;
            }
        };

        var deliveryCheckBox = new CheckBox
        {
            Content = model.AddressFieldDefaultDeliveryAddress,
            IsChecked = address.DefaultDeliveryAddress
        };
        deliveryCheckBox.Checked += (s, e) => address.DefaultDeliveryAddress = true;
        deliveryCheckBox.Unchecked += (s, e) => address.DefaultDeliveryAddress = false;

        var invoiceCheckBox = new CheckBox
        {
            Content = model.AddressFieldDefaultInvoiceAddress,
            IsChecked = address.DefaultInvoiceAddress
        };
        invoiceCheckBox.Checked += (s, e) => address.DefaultInvoiceAddress = true;
        invoiceCheckBox.Unchecked += (s, e) => address.DefaultInvoiceAddress = false;

        panel.Children.Add(firstnameBox);
        panel.Children.Add(lastnameBox);
        panel.Children.Add(companyBox);
        panel.Children.Add(streetPanel);
        panel.Children.Add(cityPanel);
        panel.Children.Add(countryComboBox);
        panel.Children.Add(deliveryCheckBox);
        panel.Children.Add(invoiceCheckBox);

        return new ContentDialog
        {
            Title = model.AddressDialogTitle,
            Content = new ScrollViewer
            {
                Content = panel,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                MaxHeight = 400
            },
            PrimaryButtonText = model.CommonSave,
            CloseButtonText = model.CommonCancel,
            DefaultButton = ContentDialogButton.Primary,
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style
        };
    }
}
