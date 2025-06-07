using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using maERP.UI.Features.Authentication.ViewModels;
using maERP.UI.Features.Dashboard.ViewModels;
using maERP.UI.Features.Customers.ViewModels;
using maERP.UI.Features.Products.ViewModels;
using maERP.UI.Features.Orders.ViewModels;
using maERP.UI.Features.Warehouses.ViewModels;
using maERP.UI.Features.SalesChannels.ViewModels;
using maERP.UI.Features.Invoices.ViewModels;
using maERP.UI.Features.AI.ViewModels;
using maERP.UI.Features.Administration.ViewModels;

namespace maERP.UI.Shared.ViewModels;

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
    private CustomerInputViewModel? _customerInputViewModel;
    private InvoiceListViewModel? _invoiceListViewModel;
    private WarehouseListViewModel? _warehouseListViewModel;
    private WarehouseDetailViewModel? _warehouseDetailViewModel;
    private ProductListViewModel? _productListViewModel;
    private ProductDetailViewModel? _productDetailViewModel;
    private AiModelListViewModel? _aiModelListViewModel;
    private AiModelDetailViewModel? _aiModelDetailViewModel;
    private AiModelInputViewModel? _aiModelInputViewModel;
    private AiPromptListViewModel? _aiPromptListViewModel;
    private AiPromptDetailViewModel? _aiPromptDetailViewModel;
    private AiPromptInputViewModel? _aiPromptInputViewModel;
    private SalesChannelListViewModel? _salesChannelListViewModel;
    private SalesChannelDetailViewModel? _salesChannelDetailViewModel;
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
            "Orders" => await GetOrderListWithRefreshAsync(),
            "Customers" => await GetCustomerListWithRefreshAsync(),
            "Invoices" => await GetInvoiceListWithRefreshAsync(),
            "Products" => await GetProductListWithRefreshAsync(),
            "Warehouses" => await GetWarehouseListWithRefreshAsync(),
            "AiModels" => await GetAiModelListWithRefreshAsync(),
            "AiPrompts" => await GetAiPromptListWithRefreshAsync(),
            "SalesChannels" => await GetSalesChannelListWithRefreshAsync(),
            "TaxClasses" => await GetTaxClassListWithRefreshAsync(),
            "Users" => await GetUserListWithRefreshAsync(),
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

    private async Task<OrderListViewModel> GetOrderListWithRefreshAsync()
    {
        var listViewModel = await GetOrderListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    private async Task<CustomerListViewModel> GetCustomerListViewModelAsync()
    {
        if (_customerListViewModel == null)
        {
            _customerListViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
            _customerListViewModel.NavigateToCustomerDetail = NavigateToCustomerDetail;
            _customerListViewModel.NavigateToCreateCustomer = NavigateToCreateCustomer;
            await _customerListViewModel.InitializeAsync();
        }
        return _customerListViewModel;
    }

    private async Task<CustomerListViewModel> GetCustomerListWithRefreshAsync()
    {
        var listViewModel = await GetCustomerListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
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

    private async Task<InvoiceListViewModel> GetInvoiceListWithRefreshAsync()
    {
        var listViewModel = await GetInvoiceListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
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

    private async Task<WarehouseListViewModel> GetWarehouseListWithRefreshAsync()
    {
        var listViewModel = await GetWarehouseListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
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

    private async Task<ProductListViewModel> GetProductListWithRefreshAsync()
    {
        var listViewModel = await GetProductListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    private async Task<AiModelListViewModel> GetAiModelListViewModelAsync()
    {
        if (_aiModelListViewModel == null)
        {
            _aiModelListViewModel = _serviceProvider.GetRequiredService<AiModelListViewModel>();
            _aiModelListViewModel.NavigateToAiModelDetail = ShowAiModelDetail;
            await _aiModelListViewModel.InitializeAsync();
        }
        return _aiModelListViewModel;
    }

    private async Task<AiModelListViewModel> GetAiModelListWithRefreshAsync()
    {
        var listViewModel = await GetAiModelListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    private async Task<AiModelDetailViewModel> GetAiModelDetailViewModelAsync(int aiModelId)
    {
        _aiModelDetailViewModel = _serviceProvider.GetRequiredService<AiModelDetailViewModel>();
        _aiModelDetailViewModel.GoBackAction = async () => await NavigateToMenuItem("AiModels");
        _aiModelDetailViewModel.NavigateToEditAiModel = NavigateToEditAiModel;
        await _aiModelDetailViewModel.InitializeAsync(aiModelId);
        return _aiModelDetailViewModel;
    }

    private async Task<AiModelInputViewModel> GetAiModelInputViewModelAsync(int aiModelId = 0)
    {
        _aiModelInputViewModel = _serviceProvider.GetRequiredService<AiModelInputViewModel>();
        _aiModelInputViewModel.GoBackAction = aiModelId > 0 
            ? async () => await NavigateToAiModelDetail(aiModelId)
            : async () => await NavigateToMenuItem("AiModels");
        _aiModelInputViewModel.NavigateToAiModelDetail = NavigateToAiModelDetail;
        await _aiModelInputViewModel.InitializeAsync(aiModelId);
        return _aiModelInputViewModel;
    }

    private async Task NavigateToEditAiModel(int aiModelId)
    {
        CurrentView = await GetAiModelInputViewModelAsync(aiModelId);
        SelectedMenuItem = "AiModels";
    }

    private async Task NavigateToAiModelDetail(int aiModelId)
    {
        CurrentView = await GetAiModelDetailViewModelAsync(aiModelId);
        SelectedMenuItem = "AiModels";
    }

    private async void ShowAiModelDetail(int aiModelId)
    {
        await NavigateToAiModelDetail(aiModelId);
    }

    private async Task<AiPromptListViewModel> GetAiPromptListViewModelAsync()
    {
        if (_aiPromptListViewModel == null)
        {
            _aiPromptListViewModel = _serviceProvider.GetRequiredService<AiPromptListViewModel>();
            _aiPromptListViewModel.NavigateToAiPromptDetail = NavigateToAiPromptDetail;
            _aiPromptListViewModel.NavigateToCreateAiPrompt = NavigateToCreateAiPrompt;
            await _aiPromptListViewModel.InitializeAsync();
        }
        return _aiPromptListViewModel;
    }

    private async Task<AiPromptListViewModel> GetAiPromptListWithRefreshAsync()
    {
        var listViewModel = await GetAiPromptListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    private async Task<SalesChannelListViewModel> GetSalesChannelListViewModelAsync()
    {
        if (_salesChannelListViewModel == null)
        {
            _salesChannelListViewModel = _serviceProvider.GetRequiredService<SalesChannelListViewModel>();
            _salesChannelListViewModel.NavigateToSalesChannelDetail = NavigateToSalesChannelDetail;
            await _salesChannelListViewModel.InitializeAsync();
        }
        return _salesChannelListViewModel;
    }

    private async Task<SalesChannelListViewModel> GetSalesChannelListWithRefreshAsync()
    {
        var listViewModel = await GetSalesChannelListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
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

    private async Task<TaxClassListViewModel> GetTaxClassListWithRefreshAsync()
    {
        var listViewModel = await GetTaxClassListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
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

    private async Task<UserListViewModel> GetUserListWithRefreshAsync()
    {
        var listViewModel = await GetUserListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    public async Task NavigateToOrderDetail(int orderId)
    {
        if (!IsAuthenticated) return;

        _orderDetailViewModel = _serviceProvider.GetRequiredService<OrderDetailViewModel>();
        _orderDetailViewModel.GoBackAction = async () => await NavigateToOrderList();
        await _orderDetailViewModel.InitializeAsync(orderId);
        
        CurrentView = _orderDetailViewModel;
        SelectedMenuItem = "OrderDetail";
    }

    [RelayCommand]
    private async Task NavigateToOrderList()
    {
        var listViewModel = await GetOrderListViewModelAsync();
        // Refresh the list to show any changes
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "Orders";
    }

    public async Task NavigateToCustomerDetail(int customerId)
    {
        if (!IsAuthenticated) return;

        _customerDetailViewModel = _serviceProvider.GetRequiredService<CustomerDetailViewModel>();
        _customerDetailViewModel.GoBackAction = async () => await NavigateToCustomerList();
        _customerDetailViewModel.NavigateToEditCustomer = NavigateToEditCustomer;
        await _customerDetailViewModel.InitializeAsync(customerId);
        
        CurrentView = _customerDetailViewModel;
        SelectedMenuItem = "CustomerDetail";
    }

    [RelayCommand]
    private async Task NavigateToCustomerList()
    {
        var listViewModel = await GetCustomerListViewModelAsync();
        // Refresh the list to show any changes
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "Customers";
    }

    public async Task NavigateToCreateCustomer()
    {
        if (!IsAuthenticated) return;

        _customerInputViewModel = _serviceProvider.GetRequiredService<CustomerInputViewModel>();
        _customerInputViewModel.GoBackAction = async () => await NavigateToCustomerList();
        _customerInputViewModel.NavigateToCustomerDetail = NavigateToCustomerDetail;
        await _customerInputViewModel.InitializeAsync(0); // 0 for new customer
        
        CurrentView = _customerInputViewModel;
        SelectedMenuItem = "CustomerInput";
    }

    public async Task NavigateToEditCustomer(int customerId)
    {
        if (!IsAuthenticated) return;

        _customerInputViewModel = _serviceProvider.GetRequiredService<CustomerInputViewModel>();
        _customerInputViewModel.GoBackAction = async () => await NavigateToCustomerDetail(customerId);
        _customerInputViewModel.NavigateToCustomerDetail = NavigateToCustomerDetail;
        await _customerInputViewModel.InitializeAsync(customerId); // Load existing customer
        
        CurrentView = _customerInputViewModel;
        SelectedMenuItem = "CustomerInput";
    }

    public async Task NavigateToProductDetail(int productId)
    {
        if (!IsAuthenticated) return;

        _productDetailViewModel = _serviceProvider.GetRequiredService<ProductDetailViewModel>();
        _productDetailViewModel.GoBackAction = async () => await NavigateToProductList();
        await _productDetailViewModel.InitializeAsync(productId);
        
        CurrentView = _productDetailViewModel;
        SelectedMenuItem = "ProductDetail";
    }

    [RelayCommand]
    private async Task NavigateToProductList()
    {
        var listViewModel = await GetProductListViewModelAsync();
        // Refresh the list to show any changes
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "Products";
    }

    public async Task NavigateToWarehouseDetail(int warehouseId)
    {
        if (!IsAuthenticated) return;

        _warehouseDetailViewModel = _serviceProvider.GetRequiredService<WarehouseDetailViewModel>();
        _warehouseDetailViewModel.GoBackAction = async () => await NavigateToWarehouseList();
        await _warehouseDetailViewModel.InitializeAsync(warehouseId);
        
        CurrentView = _warehouseDetailViewModel;
        SelectedMenuItem = "WarehouseDetail";
    }

    [RelayCommand]
    private async Task NavigateToWarehouseList()
    {
        var listViewModel = await GetWarehouseListViewModelAsync();
        // Refresh the list to show any changes
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "Warehouses";
    }

    public async Task NavigateToSalesChannelDetail(int salesChannelId)
    {
        if (!IsAuthenticated) return;

        _salesChannelDetailViewModel = _serviceProvider.GetRequiredService<SalesChannelDetailViewModel>();
        _salesChannelDetailViewModel.GoBackAction = async () => await NavigateToSalesChannelList();
        await _salesChannelDetailViewModel.InitializeAsync(salesChannelId);
        
        CurrentView = _salesChannelDetailViewModel;
        SelectedMenuItem = "SalesChannelDetail";
    }

    [RelayCommand]
    private async Task NavigateToSalesChannelList()
    {
        var listViewModel = await GetSalesChannelListViewModelAsync();
        // Refresh the list to show any changes
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "SalesChannels";
    }

    public async Task NavigateToAiPromptDetail(int aiPromptId)
    {
        if (!IsAuthenticated) return;

        _aiPromptDetailViewModel = _serviceProvider.GetRequiredService<AiPromptDetailViewModel>();
        _aiPromptDetailViewModel.GoBackAction = async () => await NavigateToAiPromptList();
        _aiPromptDetailViewModel.NavigateToEditAiPrompt = NavigateToEditAiPrompt;
        await _aiPromptDetailViewModel.InitializeAsync(aiPromptId);
        
        CurrentView = _aiPromptDetailViewModel;
        SelectedMenuItem = "AiPromptDetail";
    }

    private async Task NavigateToEditAiPrompt(int aiPromptId)
    {
        CurrentView = await GetAiPromptInputViewModelAsync(aiPromptId);
        SelectedMenuItem = "AiPrompts";
    }

    private async Task NavigateToCreateAiPrompt()
    {
        CurrentView = await GetAiPromptInputViewModelAsync();
        SelectedMenuItem = "AiPrompts";
    }

    private async Task<AiPromptInputViewModel> GetAiPromptInputViewModelAsync(int aiPromptId = 0)
    {
        _aiPromptInputViewModel = _serviceProvider.GetRequiredService<AiPromptInputViewModel>();
        _aiPromptInputViewModel.GoBackAction = aiPromptId > 0 
            ? () => { _ = NavigateToAiPromptDetail(aiPromptId); }
            : async () => await NavigateToAiPromptList();
        _aiPromptInputViewModel.NavigateToAiPromptDetail = NavigateToAiPromptDetail;
        await _aiPromptInputViewModel.InitializeAsync(aiPromptId);
        return _aiPromptInputViewModel;
    }

    [RelayCommand]
    private async Task NavigateToAiPromptList()
    {
        var listViewModel = await GetAiPromptListViewModelAsync();
        // Refresh the list to show any changes
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "AiPrompts";
    }
}
