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
using maERP.UI.Features.ImportExport.ViewModels;
using maERP.UI.Features.GoodsReceipts.ViewModels;
using maERP.UI.Features.Manufacturer.ViewModels;

namespace maERP.UI.Shared.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDebugService _debugService;

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
    private OrderInputViewModel? _orderInputViewModel;
    private CustomerListViewModel? _customerListViewModel;
    private CustomerDetailViewModel? _customerDetailViewModel;
    private CustomerInputViewModel? _customerInputViewModel;
    private InvoiceListViewModel? _invoiceListViewModel;
    private WarehouseListViewModel? _warehouseListViewModel;
    private WarehouseDetailViewModel? _warehouseDetailViewModel;
    private WarehouseInputViewModel? _warehouseInputViewModel;
    private ProductListViewModel? _productListViewModel;
    private ProductDetailViewModel? _productDetailViewModel;
    private ProductInputViewModel? _productInputViewModel;
    private AiModelListViewModel? _aiModelListViewModel;
    private AiModelDetailViewModel? _aiModelDetailViewModel;
    private AiModelInputViewModel? _aiModelInputViewModel;
    private AiPromptListViewModel? _aiPromptListViewModel;
    private AiPromptDetailViewModel? _aiPromptDetailViewModel;
    private AiPromptInputViewModel? _aiPromptInputViewModel;
    private SalesChannelListViewModel? _salesChannelListViewModel;
    private SalesChannelDetailViewModel? _salesChannelDetailViewModel;
    private SalesChannelInputViewModel? _salesChannelInputViewModel;
    private TaxClassListViewModel? _taxClassListViewModel;
    private TaxClassDetailViewModel? _taxClassDetailViewModel;
    private TaxClassInputViewModel? _taxClassInputViewModel;
    private UserListViewModel? _userListViewModel;
    private UserDetailViewModel? _userDetailViewModel;
    private UserInputViewModel? _userInputViewModel;
    private ImportExportOverviewViewModel? _importExportOverviewViewModel;
    private GoodsReceiptListViewModel? _goodsReceiptListViewModel;
    private GoodsReceiptInputViewModel? _goodsReceiptInputViewModel;
    private ManufacturerListViewModel? _manufacturerListViewModel;
    private ManufacturerDetailViewModel? _manufacturerDetailViewModel;
    private ManufacturerInputViewModel? _manufacturerInputViewModel;

    public MainViewModel(IAuthenticationService authenticationService,
                        LoginViewModel loginViewModel,
                        IServiceProvider serviceProvider,
                        IDebugService debugService)
    {
        _authenticationService = authenticationService;
        _serviceProvider = serviceProvider;
        _debugService = debugService;
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
            "ImportExport" => await GetImportExportOverviewViewModelAsync(),
            "GoodsReceipts" => await GetGoodsReceiptListWithRefreshAsync(),
            "Manufacturers" => await GetManufacturerListWithRefreshAsync(),
            _ => await GetDashboardViewModelAsync()
        };
    }

    private Task<DashboardViewModel> GetDashboardViewModelAsync()
    {
        if (_dashboardViewModel == null)
        {
            _dashboardViewModel = _serviceProvider.GetRequiredService<DashboardViewModel>();
            _dashboardViewModel.NavigateToCreateOrder = NavigateToCreateOrder;
            _dashboardViewModel.NavigateToCreateCustomer = NavigateToCreateCustomer;
            _dashboardViewModel.NavigateToMenuItem = NavigateToMenuItem;
        }
        return Task.FromResult(_dashboardViewModel);
    }

    private async Task<OrderListViewModel> GetOrderListViewModelAsync()
    {
        if (_orderListViewModel == null)
        {
            _orderListViewModel = _serviceProvider.GetRequiredService<OrderListViewModel>();
            _orderListViewModel.NavigateToOrderDetail = NavigateToOrderDetail;
            _orderListViewModel.NavigateToCreateOrder = NavigateToCreateOrder;
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
            _warehouseListViewModel.NavigateToWarehouseCreate = async () => await NavigateToWarehouseInput();
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
            _productListViewModel.NavigateToProductInput = NavigateToCreateProduct;
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
            _aiModelListViewModel.NavigateToAiModelInput = NavigateToCreateAiModel;
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

    public async Task NavigateToCreateAiModel()
    {
        if (!IsAuthenticated) return;

        _aiModelInputViewModel = _serviceProvider.GetRequiredService<AiModelInputViewModel>();
        _aiModelInputViewModel.GoBackAction = async () => await NavigateToMenuItem("AiModels");
        _aiModelInputViewModel.NavigateToAiModelDetail = NavigateToAiModelDetail;
        await _aiModelInputViewModel.InitializeAsync(0); // 0 for new ai model
        
        CurrentView = _aiModelInputViewModel;
        SelectedMenuItem = "AiModelInput";
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
            _salesChannelListViewModel.NavigateToSalesChannelInput = NavigateToSalesChannelInput;
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
            _taxClassListViewModel.NavigateToTaxClassDetail = NavigateToTaxClassDetail;
            _taxClassListViewModel.NavigateToTaxClassInput = NavigateToCreateTaxClass;
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
            _userListViewModel.NavigateToUserDetail = NavigateToUserDetail;
            _userListViewModel.NavigateToCreateUser = NavigateToCreateUser;
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
        _orderDetailViewModel.NavigateToOrderEdit = NavigateToEditOrder;
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

    public async Task NavigateToCreateOrder()
    {
        if (!IsAuthenticated) return;

        _orderInputViewModel = _serviceProvider.GetRequiredService<OrderInputViewModel>();
        _orderInputViewModel.GoBackAction = async () => await NavigateToOrderList();
        _orderInputViewModel.NavigateToOrderDetail = NavigateToOrderDetail;
        await _orderInputViewModel.InitializeAsync(0); // 0 for new order
        
        CurrentView = _orderInputViewModel;
        SelectedMenuItem = "OrderInput";
    }

    public async Task NavigateToEditOrder(int orderId)
    {
        if (!IsAuthenticated) return;

        _orderInputViewModel = _serviceProvider.GetRequiredService<OrderInputViewModel>();
        _orderInputViewModel.GoBackAction = async () => await NavigateToOrderDetail(orderId);
        _orderInputViewModel.NavigateToOrderDetail = NavigateToOrderDetail;
        await _orderInputViewModel.InitializeAsync(orderId); // Load existing order
        
        CurrentView = _orderInputViewModel;
        SelectedMenuItem = "OrderInput";
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
        _productDetailViewModel.NavigateToProductInput = NavigateToEditProduct;
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

    private async Task<ProductInputViewModel> GetProductInputViewModelAsync(int productId = 0)
    {
        _productInputViewModel = _serviceProvider.GetRequiredService<ProductInputViewModel>();
        _productInputViewModel.GoBackAction = productId > 0 
            ? async () => await NavigateToProductDetail(productId)
            : async () => await NavigateToProductList();
        _productInputViewModel.NavigateToProductDetail = NavigateToProductDetail;
        await _productInputViewModel.InitializeAsync(productId);
        return _productInputViewModel;
    }

    private async Task NavigateToEditProduct(int productId)
    {
        CurrentView = await GetProductInputViewModelAsync(productId);
        SelectedMenuItem = "Products";
    }

    private async Task NavigateToCreateProduct()
    {
        CurrentView = await GetProductInputViewModelAsync();
        SelectedMenuItem = "Products";
    }

    public void NavigateToWarehouseDetail(int warehouseId)
    {
        if (!IsAuthenticated) return;

        _warehouseDetailViewModel = _serviceProvider.GetRequiredService<WarehouseDetailViewModel>();
        _warehouseDetailViewModel.GoBackAction = async () => await NavigateToWarehouseList();
        _warehouseDetailViewModel.NavigateToEditWarehouse = async (id) => await NavigateToWarehouseInput(id);
        _ = _warehouseDetailViewModel.InitializeAsync(warehouseId);
        
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

    public async Task NavigateToWarehouseInput(int? warehouseId = null)
    {
        if (!IsAuthenticated) return;

        _warehouseInputViewModel = _serviceProvider.GetRequiredService<WarehouseInputViewModel>();
        _warehouseInputViewModel.GoBackAction = async () => await NavigateToWarehouseList();
        _warehouseInputViewModel.OnSaveSuccessAction = async () => await NavigateToWarehouseList();
        
        await _warehouseInputViewModel.InitializeAsync(warehouseId);
        
        CurrentView = _warehouseInputViewModel;
        SelectedMenuItem = "WarehouseInput";
    }

    public async Task NavigateToSalesChannelDetail(int salesChannelId)
    {
        if (!IsAuthenticated) return;

        _salesChannelDetailViewModel = _serviceProvider.GetRequiredService<SalesChannelDetailViewModel>();
        _salesChannelDetailViewModel.GoBackAction = async () => await NavigateToSalesChannelList();
        _salesChannelDetailViewModel.NavigateToSalesChannelInput = NavigateToSalesChannelInput;
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

    public async Task NavigateToSalesChannelInput(int salesChannelId)
    {
        if (!IsAuthenticated) return;

        _salesChannelInputViewModel = _serviceProvider.GetRequiredService<SalesChannelInputViewModel>();
        
        if (salesChannelId > 0)
        {
            // Edit mode
            _salesChannelInputViewModel.GoBackAction = async () => await NavigateToSalesChannelDetail(salesChannelId);
        }
        else
        {
            // Create mode
            _salesChannelInputViewModel.GoBackAction = async () => await NavigateToSalesChannelList();
        }
        
        _salesChannelInputViewModel.NavigateToSalesChannelDetail = NavigateToSalesChannelDetail;
        await _salesChannelInputViewModel.InitializeAsync(salesChannelId);
        
        CurrentView = _salesChannelInputViewModel;
        SelectedMenuItem = "SalesChannelInput";
    }

    public async Task NavigateToTaxClassDetail(int taxClassId)
    {
        if (!IsAuthenticated) return;

        _taxClassDetailViewModel = _serviceProvider.GetRequiredService<TaxClassDetailViewModel>();
        _taxClassDetailViewModel.GoBackAction = async () => await NavigateToTaxClassList();
        _taxClassDetailViewModel.NavigateToEditTaxClass = NavigateToEditTaxClass;
        await _taxClassDetailViewModel.InitializeAsync(taxClassId);
        
        CurrentView = _taxClassDetailViewModel;
        SelectedMenuItem = "TaxClassDetail";
    }

    public async Task NavigateToTaxClassList()
    {
        if (!IsAuthenticated) return;
        
        CurrentView = await GetTaxClassListWithRefreshAsync();
        SelectedMenuItem = "TaxClasses";
    }

    public async Task NavigateToCreateTaxClass()
    {
        if (!IsAuthenticated) return;

        _taxClassInputViewModel = _serviceProvider.GetRequiredService<TaxClassInputViewModel>();
        _taxClassInputViewModel.GoBackAction = async () => await NavigateToTaxClassList();
        _taxClassInputViewModel.NavigateToTaxClassDetail = NavigateToTaxClassDetail;
        await _taxClassInputViewModel.InitializeAsync(0); // 0 for new tax class
        
        CurrentView = _taxClassInputViewModel;
        SelectedMenuItem = "TaxClassInput";
    }

    public async Task NavigateToEditTaxClass(int taxClassId)
    {
        if (!IsAuthenticated) return;

        _taxClassInputViewModel = _serviceProvider.GetRequiredService<TaxClassInputViewModel>();
        _taxClassInputViewModel.GoBackAction = async () => await NavigateToTaxClassDetail(taxClassId);
        _taxClassInputViewModel.NavigateToTaxClassDetail = NavigateToTaxClassDetail;
        await _taxClassInputViewModel.InitializeAsync(taxClassId); // Load existing tax class
        
        CurrentView = _taxClassInputViewModel;
        SelectedMenuItem = "TaxClassInput";
    }

    public async Task NavigateToUserDetail(string userId)
    {
        if (!IsAuthenticated) return;

        _userDetailViewModel = _serviceProvider.GetRequiredService<UserDetailViewModel>();
        _userDetailViewModel.GoBackAction = async () => await NavigateToUserList();
        _userDetailViewModel.NavigateToEditUser = NavigateToEditUser;
        await _userDetailViewModel.InitializeAsync(userId);
        
        CurrentView = _userDetailViewModel;
        SelectedMenuItem = "UserDetail";
    }

    [RelayCommand]
    private async Task NavigateToUserList()
    {
        var listViewModel = await GetUserListViewModelAsync();
        // Refresh the list to show any changes
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "Users";
    }

    public async Task NavigateToCreateUser()
    {
        if (!IsAuthenticated) return;

        _userInputViewModel = _serviceProvider.GetRequiredService<UserInputViewModel>();
        _userInputViewModel.GoBackAction = async () => await NavigateToUserList();
        _userInputViewModel.NavigateToUserDetail = NavigateToUserDetail;
        await _userInputViewModel.InitializeAsync(""); // Empty string for new user
        
        CurrentView = _userInputViewModel;
        SelectedMenuItem = "UserInput";
    }

    public async Task NavigateToEditUser(string userId)
    {
        if (!IsAuthenticated) return;

        _userInputViewModel = _serviceProvider.GetRequiredService<UserInputViewModel>();
        _userInputViewModel.GoBackAction = async () => await NavigateToUserDetail(userId);
        _userInputViewModel.NavigateToUserDetail = NavigateToUserDetail;
        await _userInputViewModel.InitializeAsync(userId); // Load existing user
        
        CurrentView = _userInputViewModel;
        SelectedMenuItem = "UserInput";
    }

    private Task<ImportExportOverviewViewModel> GetImportExportOverviewViewModelAsync()
    {
        if (_importExportOverviewViewModel == null)
        {
            _importExportOverviewViewModel = _serviceProvider.GetRequiredService<ImportExportOverviewViewModel>();
        }
        return Task.FromResult(_importExportOverviewViewModel);
    }

    private async Task<GoodsReceiptListViewModel> GetGoodsReceiptListViewModelAsync()
    {
        if (_goodsReceiptListViewModel == null)
        {
            _goodsReceiptListViewModel = _serviceProvider.GetRequiredService<GoodsReceiptListViewModel>();
            _goodsReceiptListViewModel.NavigateToCreateGoodsReceipt = NavigateToCreateGoodsReceipt;
            await _goodsReceiptListViewModel.InitializeAsync();
        }
        return _goodsReceiptListViewModel;
    }

    private async Task<GoodsReceiptListViewModel> GetGoodsReceiptListWithRefreshAsync()
    {
        var listViewModel = await GetGoodsReceiptListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    public async Task NavigateToCreateGoodsReceipt()
    {
        if (!IsAuthenticated) return;

        _goodsReceiptInputViewModel = _serviceProvider.GetRequiredService<GoodsReceiptInputViewModel>();
        _goodsReceiptInputViewModel.GoBackAction = async () => await NavigateToGoodsReceiptList();
        await _goodsReceiptInputViewModel.InitializeAsync(0); // 0 for new goods receipt
        
        CurrentView = _goodsReceiptInputViewModel;
        SelectedMenuItem = "GoodsReceiptInput";
    }

    [RelayCommand]
    private async Task NavigateToGoodsReceiptList()
    {
        var listViewModel = await GetGoodsReceiptListViewModelAsync();
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "GoodsReceipts";
    }

    private async Task<ManufacturerListViewModel> GetManufacturerListViewModelAsync()
    {
        if (_manufacturerListViewModel == null)
        {
            _manufacturerListViewModel = _serviceProvider.GetRequiredService<ManufacturerListViewModel>();
            _manufacturerListViewModel.NavigateToManufacturerDetail = NavigateToManufacturerDetail;
            _manufacturerListViewModel.NavigateToManufacturerCreate = NavigateToCreateManufacturer;
            await _manufacturerListViewModel.InitializeAsync();
        }
        return _manufacturerListViewModel;
    }

    private async Task<ManufacturerListViewModel> GetManufacturerListWithRefreshAsync()
    {
        var listViewModel = await GetManufacturerListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    public async Task NavigateToManufacturerDetail(int manufacturerId)
    {
        if (!IsAuthenticated) return;

        _manufacturerDetailViewModel = _serviceProvider.GetRequiredService<ManufacturerDetailViewModel>();
        _manufacturerDetailViewModel.GoBackAction = async () => await NavigateToManufacturerList();
        _manufacturerDetailViewModel.NavigateToEditManufacturer = NavigateToEditManufacturer;
        await _manufacturerDetailViewModel.InitializeAsync(manufacturerId);
        
        CurrentView = _manufacturerDetailViewModel;
        SelectedMenuItem = "ManufacturerDetail";
    }

    [RelayCommand]
    private async Task NavigateToManufacturerList()
    {
        var listViewModel = await GetManufacturerListViewModelAsync();
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "Manufacturers";
    }

    public async Task NavigateToCreateManufacturer()
    {
        if (!IsAuthenticated) return;

        _manufacturerInputViewModel = _serviceProvider.GetRequiredService<ManufacturerInputViewModel>();
        _manufacturerInputViewModel.GoBackAction = async () => await NavigateToManufacturerList();
        _manufacturerInputViewModel.NavigateToManufacturerDetail = NavigateToManufacturerDetail;
        await _manufacturerInputViewModel.InitializeAsync(0); // 0 for new manufacturer
        
        CurrentView = _manufacturerInputViewModel;
        SelectedMenuItem = "ManufacturerInput";
    }

    public async Task NavigateToEditManufacturer(int manufacturerId)
    {
        if (!IsAuthenticated) return;

        _manufacturerInputViewModel = _serviceProvider.GetRequiredService<ManufacturerInputViewModel>();
        _manufacturerInputViewModel.GoBackAction = async () => await NavigateToManufacturerDetail(manufacturerId);
        _manufacturerInputViewModel.NavigateToManufacturerDetail = NavigateToManufacturerDetail;
        await _manufacturerInputViewModel.InitializeAsync(manufacturerId); // Load existing manufacturer
        
        CurrentView = _manufacturerInputViewModel;
        SelectedMenuItem = "ManufacturerInput";
    }

    [RelayCommand]
    public void ToggleDebugWindow()
    {
        _debugService.ToggleDebugWindow();
        _debugService.LogInfo("Debug window toggled via F12 key");
    }
}
