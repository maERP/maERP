using System.Collections.Generic;
using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class ProductsDetailPage : ContentPage
{
	private readonly ProductsDetailViewModel _viewModel;
	readonly int _productId;

	public ProductsDetailPage(int productId, ProductsDetailViewModel viewModel)
	{
		this._viewModel = viewModel;
		this._productId = productId;

		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		GetProduct();
	}

	public async void GetProduct()
	{
		BindingContext = await _viewModel.GetProduct();
	}

	private void SaveProductButton_OnClicked(object sender, EventArgs e)
	{
		Console.WriteLine("Save Command");
	}
}