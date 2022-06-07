using System.Collections.Generic;
using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class ProductsDetailPage : ContentPage
{
	private readonly ProductsDetailViewModel _viewModel;

	public ProductsDetailPage(ProductsDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		this._viewModel = viewModel;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();

		await _viewModel.GetProductDetailAsync();
	}
}