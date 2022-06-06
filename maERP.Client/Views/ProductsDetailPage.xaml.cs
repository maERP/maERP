using System.Collections.Generic;
using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class ProductsDetailPage : ContentPage
{
	public ProductsDetailPage(ProductsDetailViewModel viewModel)
	{
		Console.WriteLine("DEBUG 6");
		InitializeComponent();

		Console.WriteLine("DEBUG 7");
		 BindingContext = viewModel;
		Console.WriteLine("DEBUG 8");
	}
}