using System.Collections.Generic;
using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class ProductsDetailPage : ContentPage
{
	public ProductsDetailPage(ProductsDetailViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}