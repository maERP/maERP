using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private bool isAuthenticated;

    [ObservableProperty]
    private ViewModelBase? currentView;

    [ObservableProperty]
    private string selectedMenuItem = "Dashboard";

    public LoginViewModel LoginViewModel { get; }
    
    private DashboardViewModel? _dashboardViewModel;
    private OrderListViewModel? _orderListViewModel;
    private OrderDetailViewModel? _orderDetailViewModel;
    private CustomerListViewModel? _customerListViewModel;
    private CustomerDetailViewModel? _customerDetailViewModel;
    private InvoiceListViewModel? _invoiceListViewModel;
    private WarehouseListViewModel? _warehouseListViewModel;
    private WarehouseDetailViewModel? _warehouseDetailViewModel;
    private ProductListViewModel? _productListViewModel;
    private ProductDetailViewModel? _productDetailViewModel;
    private AiModelListViewModel? _aiModelListViewModel;
    private AiPromptListViewModel? _aiPromptListViewModel;
    private SalesChannelListViewModel? _salesChannelListViewModel;
    private TaxClassListViewModel? _taxClassListViewModel;
    private UserListViewModel? _userListViewModel;

    public MainViewModel(IAuthenticationService authenticationService,
                        LoginViewModel loginViewModel,
                        IServiceProvider serviceProvider)
    {
        _authenticationService = authenticationService;
        _serviceProvider = serviceProvider;
        LoginViewModel = loginViewModel;

        LoginViewModel.OnLoginSuccessful += OnLoginSuccessful;

        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Versuche Auto-Login wenn gespeicherte Credentials vorhanden sind
        var autoLoginSuccessful = await LoginViewModel.TryAutoLoginAsync();

        if (autoLoginSuccessful)
        {
            IsAuthenticated = true;
            CurrentView = await GetDashboardViewModelAsync();
            SelectedMenuItem = "Dashboard";
        }
        else
        {
            IsAuthenticated = _authenticationService.IsAuthenticated;
            CurrentView = IsAuthenticated ? await GetDashboardViewModelAsync() : LoginViewModel;
        }
    }

    private async void OnLoginSuccessful()
    {
        IsAuthenticated = true;
        CurrentView = await GetDashboardViewModelAsync();
        SelectedMenuItem = "Dashboard";
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authenticationService.LogoutAsync();
        IsAuthenticated = false;
        CurrentView = LoginViewModel;
        SelectedMenuItem = "";
    }

    [RelayCommand]
    private async Task NavigateToMenuItem(string menuItem)
    {
        if (!IsAuthenticated) return;

        SelectedMenuItem = menuItem;

        CurrentView = menuItem switch
        {
            "Dashboard" => await GetDashboardViewModelAsync(),
            "Orders" => await GetOrderListViewModelAsync(),
            "Customers" => await GetCustomerListViewModelAsync(),
            "Invoices" => await GetInvoiceListViewModelAsync(),
            "Products" => await GetProductListViewModelAsync(),
            "Warehouses" => await GetWarehouseListViewModelAsync(),
            "AiModels" => await GetAiModelListViewModelAsync(),
            "AiPrompts" => await GetAiPromptListViewModelAsync(),
            "SalesChannels" => await GetSalesChannelListViewModelAsync(),
            "TaxClasses" => await GetTaxClassListViewModelAsync(),
            "Users" => await GetUserListViewModelAsync(),
            _ => await GetDashboardViewModelAsync()
        };
    }

    private Task<DashboardViewModel> GetDashboardViewModelAsync()
    {
        if (_dashboardViewModel == null)
        {
            _dashboardViewModel = _serviceProvider.GetRequiredService<DashboardViewModel>();
        }
        return Task.FromResult(_dashboardViewModel);
    }

    private async Task<OrderListViewModel> GetOrderListViewModelAsync()
    {
        if (_orderListViewModel == null)
        {
            _orderListViewModel = _serviceProvider.GetRequiredService<OrderListViewModel>();
            _orderListViewModel.NavigateToOrderDetail = NavigateToOrderDetail;
            await _orderListViewModel.InitializeAsync();
        }
        return _orderListViewModel;
    }

    private async Task<CustomerListViewModel> GetCustomerListViewModelAsync()
    {
        if (_customerListViewModel == null)
        {
            _customerListViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
            _customerListViewModel.NavigateToCustomerDetail = NavigateToCustomerDetail;
            await _customerListViewModel.InitializeAsync();
        }
        return _customerListViewModel;
    }

    private async Task<InvoiceListViewModel> GetInvoiceListViewModelAsync()
    {
        if (_invoiceListViewModel == null)
        {
            _invoiceListViewModel = _serviceProvider.GetRequiredService<InvoiceListViewModel>();
            await _invoiceListViewModel.InitializeAsync();
        }
        return _invoiceListViewModel;
    }

    private async Task<WarehouseListViewModel> GetWarehouseListViewModelAsync()
    {
        if (_warehouseListViewModel == null)
        {
            _warehouseListViewModel = _serviceProvider.GetRequiredService<WarehouseListViewModel>();
            _warehouseListViewModel.NavigateToWarehouseDetail = NavigateToWarehouseDetail;
            await _warehouseListViewModel.InitializeAsync();
        }
        return _warehouseListViewModel;
    }

    private async Task<ProductListViewModel> GetProductListViewModelAsync()
    {
        if (_productListViewModel == null)
        {
            _productListViewModel = _serviceProvider.GetRequiredService<ProductListViewModel>();
            _productListViewModel.NavigateToProductDetail = NavigateToProductDetail;
            await _productListViewModel.InitializeAsync();
        }
        return _productListViewModel;
    }

    private async Task<AiModelListViewModel> GetAiModelListViewModelAsync()
    {
        if (_aiModelListViewModel == null)
        {
            _aiModelListViewModel = _serviceProvider.GetRequiredService<AiModelListViewModel>();
            await _aiModelListViewModel.InitializeAsync();
        }
        return _aiModelListViewModel;
    }

    private async Task<AiPromptListViewModel> GetAiPromptListViewModelAsync()
    {
        if (_aiPromptListViewModel == null)
        {
            _aiPromptListViewModel = _serviceProvider.GetRequiredService<AiPromptListViewModel>();
            await _aiPromptListViewModel.InitializeAsync();
        }
        return _aiPromptListViewModel;
    }

    private async Task<SalesChannelListViewModel> GetSalesChannelListViewModelAsync()
    {
        if (_salesChannelListViewModel == null)
        {
            _salesChannelListViewModel = _serviceProvider.GetRequiredService<SalesChannelListViewModel>();
            await _salesChannelListViewModel.InitializeAsync();
        }
        return _salesChannelListViewModel;
    }

    private async Task<TaxClassListViewModel> GetTaxClassListViewModelAsync()
    {
        if (_taxClassListViewModel == null)
        {
            _taxClassListViewModel = _serviceProvider.GetRequiredService<TaxClassListViewModel>();
            await _taxClassListViewModel.InitializeAsync();
        }
        return _taxClassListViewModel;
    }

    private async Task<UserListViewModel> GetUserListViewModelAsync()
    {
        if (_userListViewModel == null)
        {
            _userListViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
            await _userListViewModel.InitializeAsync();
        }
        return _userListViewModel;
    }

    public async Task NavigateToOrderDetail(int orderId)
    {
        if (!IsAuthenticated) return;

        _orderDetailViewModel = _serviceProvider.GetRequiredService<OrderDetailViewModel>();
        _orderDetailViewModel.GoBackAction = () => NavigateToOrderList();
        await _orderDetailViewModel.InitializeAsync(orderId);
        
        CurrentView = _orderDetailViewModel;
        SelectedMenuItem = "OrderDetail";
    }

    [RelayCommand]
    private async Task NavigateToOrderList()
    {
        CurrentView = await GetOrderListViewModelAsync();
        SelectedMenuItem = "Orders";
    }

    public async Task NavigateToCustomerDetail(int customerId)
    {
        if (!IsAuthenticated) return;

        _customerDetailViewModel = _serviceProvider.GetRequiredService<CustomerDetailViewModel>();
        _customerDetailViewModel.GoBackAction = () => NavigateToCustomerList();
        await _customerDetailViewModel.InitializeAsync(customerId);
        
        CurrentView = _customerDetailViewModel;
        SelectedMenuItem = "CustomerDetail";
    }

    [RelayCommand]
    private async Task NavigateToCustomerList()
    {
        CurrentView = await GetCustomerListViewModelAsync();
        SelectedMenuItem = "Customers";
    }

    public async Task NavigateToProductDetail(int productId)
    {
        if (!IsAuthenticated) return;

        _productDetailViewModel = _serviceProvider.GetRequiredService<ProductDetailViewModel>();
        _productDetailViewModel.GoBackAction = () => NavigateToProductList();
        await _productDetailViewModel.InitializeAsync(productId);
        
        CurrentView = _productDetailViewModel;
        SelectedMenuItem = "ProductDetail";
    }

    [RelayCommand]
    private async Task NavigateToProductList()
    {
        CurrentView = await GetProductListViewModelAsync();
        SelectedMenuItem = "Products";
    }

    public async Task NavigateToWarehouseDetail(int warehouseId)
    {
        if (!IsAuthenticated) return;

        _warehouseDetailViewModel = _serviceProvider.GetRequiredService<WarehouseDetailViewModel>();
        _warehouseDetailViewModel.GoBackAction = () => NavigateToWarehouseList();
        await _warehouseDetailViewModel.InitializeAsync(warehouseId);
        
        CurrentView = _warehouseDetailViewModel;
        SelectedMenuItem = "WarehouseDetail";
    }

    [RelayCommand]
    private async Task NavigateToWarehouseList()
    {
        CurrentView = await GetWarehouseListViewModelAsync();
        SelectedMenuItem = "Warehouses";
    }
}
