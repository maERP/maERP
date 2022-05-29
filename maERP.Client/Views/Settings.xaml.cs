using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
	}
}