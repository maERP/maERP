using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
// TODO: Re-implement theme switching for Uno Platform using ElementTheme
// using Microsoft.UI.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using maERP.UI.Shared.Features.Authentication.ViewModels;
using maERP.UI.Shared.Features.Dashboard.ViewModels;
using maERP.UI.Shared.Features.Customers.ViewModels;
using maERP.UI.Shared.Features.Products.ViewModels;
using maERP.UI.Shared.Features.Orders.ViewModels;
using maERP.UI.Shared.Features.Warehouses.ViewModels;
using maERP.UI.Shared.Features.SalesChannels.ViewModels;
using maERP.UI.Shared.Features.Invoices.ViewModels;
using maERP.UI.Shared.Features.AI.ViewModels;
using maERP.UI.Shared.Features.Administration.ViewModels;
using maERP.UI.Shared.Features.ImportExport.ViewModels;
using maERP.UI.Shared.Features.GoodsReceipts.ViewModels;
using maERP.UI.Shared.Features.Manufacturer.ViewModels;
using SuperadminTenantsListViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminTenantsListViewModel;
using SuperadminTenantsDetailViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminTenantsDetailViewModel;
using SuperadminTenantsInputViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminTenantsInputViewModel;
using SuperadminUserListViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminUserListViewModel;
using SuperadminUserDetailViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminUserDetailViewModel;
using SuperadminUserInputViewModel = maERP.UI.Shared.Features.Superadmin.ViewModels.SuperadminUserInputViewModel;
using UserTenantListViewModel = maERP.UI.Shared.Features.Tenant.ViewModels.TenantListViewModel;
using UserTenantDetailViewModel = maERP.UI.Shared.Features.Tenant.ViewModels.TenantDetailViewModel;
using UserTenantInputViewModel = maERP.UI.Shared.Features.Tenant.ViewModels.TenantInputViewModel;

namespace maERP.UI.Shared.Shared.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private bool isAuthenticated;

    [ObservableProperty]
    private bool showMainApplication;

    [ObservableProperty]
    private ObservableObject? currentView;

    [ObservableProperty]
    private string selectedMenuItem = "Dashboard";

    [ObservableProperty]
    private bool isSuperAdmin;

    [ObservableProperty]
    private bool isDarkTheme;

    public LoginViewModel LoginViewModel { get; }
    public RegistrationViewModel RegistrationViewModel { get; }
    public ForgotPasswordViewModel ForgotPasswordViewModel { get; }
    public ResetPasswordViewModel ResetPasswordViewModel { get; }
    public TenantSetupViewModel TenantSetupViewModel { get; }
    
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
    private SuperadminUserListViewModel? _superadminUserListViewModel;
    private SuperadminUserDetailViewModel? _superadminUserDetailViewModel;
    private SuperadminUserInputViewModel? _superadminUserInputViewModel;
    private ImportExportOverviewViewModel? _importExportOverviewViewModel;
    private GoodsReceiptListViewModel? _goodsReceiptListViewModel;
    private GoodsReceiptInputViewModel? _goodsReceiptInputViewModel;
    private ManufacturerListViewModel? _manufacturerListViewModel;
    private ManufacturerDetailViewModel? _manufacturerDetailViewModel;
    private ManufacturerInputViewModel? _manufacturerInputViewModel;
    private SuperadminTenantsListViewModel? _superadminTenantsListViewModel;
    private SuperadminTenantsDetailViewModel? _superadminTenantsDetailViewModel;
    private SuperadminTenantsInputViewModel? _superadminTenantsInputViewModel;
    private UserTenantListViewModel? _tenantListViewModel;
    private UserTenantDetailViewModel? _tenantDetailViewModel;
    private UserTenantInputViewModel? _tenantInputViewModel;

    public MainViewModel(IAuthenticationService authenticationService,
                        LoginViewModel loginViewModel,
                        RegistrationViewModel registrationViewModel,
                        ForgotPasswordViewModel forgotPasswordViewModel,
                        ResetPasswordViewModel resetPasswordViewModel,
                        TenantSetupViewModel tenantSetupViewModel,
                        IServiceProvider serviceProvider,
                        IDebugService debugService)
    {
        _authenticationService = authenticationService;
        _serviceProvider = serviceProvider;
        _debugService = debugService;
        LoginViewModel = loginViewModel;
        RegistrationViewModel = registrationViewModel;
        ForgotPasswordViewModel = forgotPasswordViewModel;
        ResetPasswordViewModel = resetPasswordViewModel;
        TenantSetupViewModel = tenantSetupViewModel;

        // Set initial view to prevent null reference when UI renders before InitializeAsync completes
        // This ensures the login view is displayed immediately, especially important for Release builds
        CurrentView = LoginViewModel;

        LoginViewModel.OnLoginSuccessful += OnLoginSuccessful;
        LoginViewModel.OnShowRegistration += OnShowRegistration;
        LoginViewModel.OnShowForgotPassword += OnShowForgotPassword;
        RegistrationViewModel.OnRegistrationSuccessful += OnRegistrationSuccessful;
        RegistrationViewModel.OnBackToLogin += OnBackToLogin;
        ForgotPasswordViewModel.OnBackToLogin += OnBackToLogin;
        ResetPasswordViewModel.OnBackToLogin += OnBackToLogin;
        ResetPasswordViewModel.OnPasswordResetSuccess += OnPasswordResetSuccess;
        TenantSetupViewModel.OnTenantCreated += OnTenantCreated;
        TenantSetupViewModel.OnLogout += OnTenantSetupLogout;

        InitializeTheme();
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Versuche Auto-Login wenn gespeicherte Credentials vorhanden sind
        var autoLoginSuccessful = await LoginViewModel.TryAutoLoginAsync();

        if (autoLoginSuccessful)
        {
            IsAuthenticated = true;
            UpdateRoleFlags();

            // Check for tenants
            var availableTenants = _authenticationService.AvailableTenants;
            if (availableTenants != null && availableTenants.Count > 0)
            {
                ShowMainApplication = true;
                CurrentView = await GetDashboardViewModelAsync();
                SelectedMenuItem = "Dashboard";
            }
            else
            {
                ShowMainApplication = false;
                CurrentView = TenantSetupViewModel;
                SelectedMenuItem = "TenantSetup";
            }
        }
        else
        {
            IsAuthenticated = _authenticationService.IsAuthenticated;
            UpdateRoleFlags();

            if (IsAuthenticated)
            {
                var availableTenants = _authenticationService.AvailableTenants;
                if (availableTenants != null && availableTenants.Count > 0)
                {
                    ShowMainApplication = true;
                    CurrentView = await GetDashboardViewModelAsync();
                }
                else
                {
                    ShowMainApplication = false;
                    CurrentView = TenantSetupViewModel;
                }
            }
            else
            {
                ShowMainApplication = false;
                CurrentView = LoginViewModel;
            }
        }
    }

    private void UpdateRoleFlags()
    {
        IsSuperAdmin = false;

        var token = _authenticationService.Token;
        if (string.IsNullOrWhiteSpace(token))
        {
            return;
        }

        try
        {
            var parts = token.Split('.');
            if (parts.Length < 2)
            {
                return;
            }

            var payload = parts[1]
                .Replace('-', '+')
                .Replace('_', '/');
            var paddedPayload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            var bytes = Convert.FromBase64String(paddedPayload);
            using var document = JsonDocument.Parse(bytes);

            foreach (var value in ExtractRoleLikeValues(document.RootElement))
            {
                if (string.Equals(value, "Superadmin", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(value, "SuperAdmin", StringComparison.OrdinalIgnoreCase))
                {
                    IsSuperAdmin = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            _debugService.LogDebug($"Failed to parse roles from token: {ex.Message}");
        }
    }

    private static IEnumerable<string> ExtractRoleLikeValues(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var property in element.EnumerateObject())
                {
                    if (property.Name.IndexOf("role", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        property.Name.IndexOf("permission", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        foreach (var value in ExtractStringValues(property.Value))
                        {
                            yield return value;
                        }
                    }
                    else
                    {
                        foreach (var nested in ExtractRoleLikeValues(property.Value))
                        {
                            yield return nested;
                        }
                    }
                }
                break;
            case JsonValueKind.Array:
                foreach (var item in element.EnumerateArray())
                {
                    foreach (var nested in ExtractRoleLikeValues(item))
                    {
                        yield return nested;
                    }
                }
                break;
        }
    }

    private static IEnumerable<string> ExtractStringValues(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.String:
                var value = element.GetString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    yield return value;
                }
                break;
            case JsonValueKind.Array:
                foreach (var item in element.EnumerateArray())
                {
                    foreach (var nested in ExtractStringValues(item))
                    {
                        yield return nested;
                    }
                }
                break;
            case JsonValueKind.Object:
                foreach (var property in element.EnumerateObject())
                {
                    foreach (var nested in ExtractStringValues(property.Value))
                    {
                        yield return nested;
                    }
                }
                break;
        }
    }

    private async void OnLoginSuccessful()
    {
        IsAuthenticated = true;
        UpdateRoleFlags();

        // Check if user has any tenants
        var availableTenants = _authenticationService.AvailableTenants;
        if (availableTenants == null || availableTenants.Count == 0)
        {
            // No tenants available - show tenant setup view without main application UI
            _debugService.LogInfo("User has no tenants - navigating to tenant setup");
            ShowMainApplication = false;
            CurrentView = TenantSetupViewModel;
            SelectedMenuItem = "TenantSetup";
        }
        else
        {
            // User has tenants - navigate to dashboard with main application UI
            _debugService.LogInfo($"User has {availableTenants.Count} tenant(s) - navigating to dashboard");
            ShowMainApplication = true;
            CurrentView = await GetDashboardViewModelAsync();
            SelectedMenuItem = "Dashboard";
        }
    }

    private void OnShowRegistration()
    {
        CurrentView = RegistrationViewModel;
    }

    private void OnRegistrationSuccessful()
    {
        CurrentView = LoginViewModel;
    }

    private void OnBackToLogin()
    {
        CurrentView = LoginViewModel;
    }

    private void OnShowForgotPassword()
    {
        // Pass the ServerUrl from LoginViewModel to ForgotPasswordViewModel
        ForgotPasswordViewModel.SetServerUrl(LoginViewModel.ServerUrl);
        CurrentView = ForgotPasswordViewModel;
    }

    private void OnPasswordResetSuccess()
    {
        CurrentView = LoginViewModel;
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authenticationService.LogoutAsync();
        IsAuthenticated = false;
        ShowMainApplication = false;
        IsSuperAdmin = false;
        CurrentView = LoginViewModel;
        SelectedMenuItem = "";
    }

    [RelayCommand]
    private async Task NavigateToMenuItem(string menuItem)
    {
        if (!IsAuthenticated) return;

        if (menuItem == "Tenants" && !IsSuperAdmin)
        {
            _debugService.LogWarning("Navigation to Tenants blocked: missing Superadmin permission");
            return;
        }

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
            "Tenants" => await GetSuperadminTenantsListWithRefreshAsync(),
            "MyTenants" => await GetTenantListWithRefreshAsync(),
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

    private async Task<SuperadminTenantsListViewModel> GetSuperadminTenantsListViewModelAsync()
    {
        if (_superadminTenantsListViewModel == null)
        {
            _superadminTenantsListViewModel = _serviceProvider.GetRequiredService<SuperadminTenantsListViewModel>();
            _superadminTenantsListViewModel.NavigateToTenantDetail = NavigateToTenantDetail;
            _superadminTenantsListViewModel.NavigateToTenantInput = NavigateToCreateTenant;
            await _superadminTenantsListViewModel.InitializeAsync();
        }
        return _superadminTenantsListViewModel;
    }

    private async Task<SuperadminTenantsListViewModel> GetSuperadminTenantsListWithRefreshAsync()
    {
        var listViewModel = await GetSuperadminTenantsListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    private async Task<SuperadminTenantsDetailViewModel> GetSuperadminTenantsDetailViewModelAsync(Guid tenantId)
    {
        _superadminTenantsDetailViewModel = _serviceProvider.GetRequiredService<SuperadminTenantsDetailViewModel>();
        _superadminTenantsDetailViewModel.GoBackAction = async () => await NavigateToTenantList();
        _superadminTenantsDetailViewModel.NavigateToTenantEdit = NavigateToEditTenant;
        await _superadminTenantsDetailViewModel.InitializeAsync(tenantId);
        return _superadminTenantsDetailViewModel;
    }

    private async Task<SuperadminTenantsInputViewModel> GetSuperadminTenantsInputViewModelAsync(Guid? tenantId = null)
    {
        _superadminTenantsInputViewModel = _serviceProvider.GetRequiredService<SuperadminTenantsInputViewModel>();
        if (tenantId == null || tenantId == Guid.Empty)
        {
            _superadminTenantsInputViewModel.GoBackAction = async () => await NavigateToTenantList();
        }
        else
        {
            var capturedId = tenantId;
            _superadminTenantsInputViewModel.GoBackAction = async () => await NavigateToTenantDetail(capturedId.Value);
        }

        _superadminTenantsInputViewModel.NavigateToTenantDetail = NavigateToTenantDetail;
        await _superadminTenantsInputViewModel.InitializeAsync(tenantId ?? Guid.Empty);
        return _superadminTenantsInputViewModel;
    }

    public async Task NavigateToTenantDetail(Guid tenantId)
    {
        if (!IsAuthenticated || tenantId == Guid.Empty) return;

        var detailViewModel = await GetSuperadminTenantsDetailViewModelAsync(tenantId);
        CurrentView = detailViewModel;
        SelectedMenuItem = "TenantDetail";
    }

    public async Task NavigateToCreateTenant()
    {
        if (!IsAuthenticated) return;

        var inputViewModel = await GetSuperadminTenantsInputViewModelAsync();
        CurrentView = inputViewModel;
        SelectedMenuItem = "TenantInput";
    }

    public async Task NavigateToEditTenant(Guid tenantId)
    {
        if (!IsAuthenticated || tenantId == Guid.Empty) return;

        var inputViewModel = await GetSuperadminTenantsInputViewModelAsync(tenantId);
        CurrentView = inputViewModel;
        SelectedMenuItem = "TenantInput";
    }

    [RelayCommand]
    private async Task NavigateToTenantList()
    {
        var listViewModel = await GetSuperadminTenantsListViewModelAsync();
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "Tenants";
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

    private async Task<AiModelDetailViewModel> GetAiModelDetailViewModelAsync(Guid aiModelId)
    {
        _aiModelDetailViewModel = _serviceProvider.GetRequiredService<AiModelDetailViewModel>();
        _aiModelDetailViewModel.GoBackAction = async () => await NavigateToMenuItem("AiModels");
        _aiModelDetailViewModel.NavigateToEditAiModel = NavigateToEditAiModel;
        await _aiModelDetailViewModel.InitializeAsync(aiModelId);
        return _aiModelDetailViewModel;
    }

    private async Task<AiModelInputViewModel> GetAiModelInputViewModelAsync(Guid aiModelId = default)
    {
        _aiModelInputViewModel = _serviceProvider.GetRequiredService<AiModelInputViewModel>();
        _aiModelInputViewModel.GoBackAction = aiModelId != Guid.Empty 
            ? async () => await NavigateToAiModelDetail(aiModelId)
            : async () => await NavigateToMenuItem("AiModels");
        _aiModelInputViewModel.NavigateToAiModelDetail = NavigateToAiModelDetail;
        await _aiModelInputViewModel.InitializeAsync(aiModelId);
        return _aiModelInputViewModel;
    }

    private async Task NavigateToEditAiModel(Guid aiModelId)
    {
        CurrentView = await GetAiModelInputViewModelAsync(aiModelId);
        SelectedMenuItem = "AiModels";
    }

    private async Task NavigateToAiModelDetail(Guid aiModelId)
    {
        CurrentView = await GetAiModelDetailViewModelAsync(aiModelId);
        SelectedMenuItem = "AiModels";
    }

    private async void ShowAiModelDetail(Guid aiModelId)
    {
        await NavigateToAiModelDetail(aiModelId);
    }

    public async Task NavigateToCreateAiModel()
    {
        if (!IsAuthenticated) return;

        _aiModelInputViewModel = _serviceProvider.GetRequiredService<AiModelInputViewModel>();
        _aiModelInputViewModel.GoBackAction = async () => await NavigateToMenuItem("AiModels");
        _aiModelInputViewModel.NavigateToAiModelDetail = NavigateToAiModelDetail;
        await _aiModelInputViewModel.InitializeAsync(Guid.Empty);
        
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

    private async Task<SuperadminUserListViewModel> GetUserListViewModelAsync()
    {
        if (_superadminUserListViewModel == null)
        {
            _superadminUserListViewModel = _serviceProvider.GetRequiredService<SuperadminUserListViewModel>();
            _superadminUserListViewModel.NavigateToUserDetail = NavigateToUserDetail;
            _superadminUserListViewModel.NavigateToCreateUser = NavigateToCreateUser;
            await _superadminUserListViewModel.InitializeAsync();
        }
        return _superadminUserListViewModel;
    }

    private async Task<SuperadminUserListViewModel> GetUserListWithRefreshAsync()
    {
        var listViewModel = await GetUserListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    public async Task NavigateToOrderDetail(Guid orderId)
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
        await _orderInputViewModel.InitializeAsync(Guid.Empty);
        
        CurrentView = _orderInputViewModel;
        SelectedMenuItem = "OrderInput";
    }

    public async Task NavigateToEditOrder(Guid orderId)
    {
        if (!IsAuthenticated) return;

        _orderInputViewModel = _serviceProvider.GetRequiredService<OrderInputViewModel>();
        _orderInputViewModel.GoBackAction = async () => await NavigateToOrderDetail(orderId);
        _orderInputViewModel.NavigateToOrderDetail = NavigateToOrderDetail;
        await _orderInputViewModel.InitializeAsync(orderId); // Load existing order
        
        CurrentView = _orderInputViewModel;
        SelectedMenuItem = "OrderInput";
    }

    public async Task NavigateToCustomerDetail(Guid customerId)
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
        await _customerInputViewModel.InitializeAsync(Guid.Empty);
        
        CurrentView = _customerInputViewModel;
        SelectedMenuItem = "CustomerInput";
    }

    public async Task NavigateToEditCustomer(Guid customerId)
    {
        if (!IsAuthenticated) return;

        _customerInputViewModel = _serviceProvider.GetRequiredService<CustomerInputViewModel>();
        _customerInputViewModel.GoBackAction = async () => await NavigateToCustomerDetail(customerId);
        _customerInputViewModel.NavigateToCustomerDetail = NavigateToCustomerDetail;
        await _customerInputViewModel.InitializeAsync(customerId); // Load existing customer
        
        CurrentView = _customerInputViewModel;
        SelectedMenuItem = "CustomerInput";
    }

    public async Task NavigateToProductDetail(Guid productId)
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

    private async Task<ProductInputViewModel> GetProductInputViewModelAsync(Guid productId = default)
    {
        _productInputViewModel = _serviceProvider.GetRequiredService<ProductInputViewModel>();
        _productInputViewModel.GoBackAction = productId != Guid.Empty 
            ? async () => await NavigateToProductDetail(productId)
            : async () => await NavigateToProductList();
        _productInputViewModel.NavigateToProductDetail = NavigateToProductDetail;
        await _productInputViewModel.InitializeAsync(productId);
        return _productInputViewModel;
    }

    private async Task NavigateToEditProduct(Guid productId)
    {
        CurrentView = await GetProductInputViewModelAsync(productId);
        SelectedMenuItem = "Products";
    }

    private async Task NavigateToCreateProduct()
    {
        CurrentView = await GetProductInputViewModelAsync();
        SelectedMenuItem = "Products";
    }

    public void NavigateToWarehouseDetail(Guid warehouseId)
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

    public async Task NavigateToWarehouseInput(Guid? warehouseId = null)
    {
        if (!IsAuthenticated) return;

        _warehouseInputViewModel = _serviceProvider.GetRequiredService<WarehouseInputViewModel>();
        _warehouseInputViewModel.GoBackAction = async () => await NavigateToWarehouseList();
        _warehouseInputViewModel.OnSaveSuccessAction = async () => await NavigateToWarehouseList();
        
        await _warehouseInputViewModel.InitializeAsync(warehouseId);
        
        CurrentView = _warehouseInputViewModel;
        SelectedMenuItem = "WarehouseInput";
    }

    public async Task NavigateToSalesChannelDetail(Guid salesChannelId)
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

    public async Task NavigateToAiPromptDetail(Guid aiPromptId)
    {
        if (!IsAuthenticated) return;

        _aiPromptDetailViewModel = _serviceProvider.GetRequiredService<AiPromptDetailViewModel>();
        _aiPromptDetailViewModel.GoBackAction = async () => await NavigateToAiPromptList();
        _aiPromptDetailViewModel.NavigateToEditAiPrompt = NavigateToEditAiPrompt;
        await _aiPromptDetailViewModel.InitializeAsync(aiPromptId);

        CurrentView = _aiPromptDetailViewModel;
        SelectedMenuItem = "AiPromptDetail";
    }

    private async Task NavigateToEditAiPrompt(Guid aiPromptId)
    {
        CurrentView = await GetAiPromptInputViewModelAsync(aiPromptId);
        SelectedMenuItem = "AiPrompts";
    }

    private async Task NavigateToCreateAiPrompt()
    {
        CurrentView = await GetAiPromptInputViewModelAsync();
        SelectedMenuItem = "AiPrompts";
    }

    private async Task<AiPromptInputViewModel> GetAiPromptInputViewModelAsync(Guid aiPromptId = default)
    {
        _aiPromptInputViewModel = _serviceProvider.GetRequiredService<AiPromptInputViewModel>();
        _aiPromptInputViewModel.GoBackAction = aiPromptId != Guid.Empty 
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

    public async Task NavigateToSalesChannelInput(Guid salesChannelId)
    {
        if (!IsAuthenticated) return;

        _salesChannelInputViewModel = _serviceProvider.GetRequiredService<SalesChannelInputViewModel>();
        
        if (salesChannelId != Guid.Empty)
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

    public async Task NavigateToTaxClassDetail(Guid taxClassId)
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
        await _taxClassInputViewModel.InitializeAsync(Guid.Empty);
        
        CurrentView = _taxClassInputViewModel;
        SelectedMenuItem = "TaxClassInput";
    }

    public async Task NavigateToEditTaxClass(Guid taxClassId)
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

        _superadminUserDetailViewModel = _serviceProvider.GetRequiredService<SuperadminUserDetailViewModel>();
        _superadminUserDetailViewModel.GoBackAction = async () => await NavigateToUserList();
        _superadminUserDetailViewModel.NavigateToEditUser = NavigateToEditUser;
        await _superadminUserDetailViewModel.InitializeAsync(userId);

        CurrentView = _superadminUserDetailViewModel;
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

        _superadminUserInputViewModel = _serviceProvider.GetRequiredService<SuperadminUserInputViewModel>();
        _superadminUserInputViewModel.GoBackAction = async () => await NavigateToUserList();
        _superadminUserInputViewModel.NavigateToUserDetail = NavigateToUserDetail;
        await _superadminUserInputViewModel.InitializeAsync(""); // Empty string for new user

        CurrentView = _superadminUserInputViewModel;
        SelectedMenuItem = "UserInput";
    }

    public async Task NavigateToEditUser(string userId)
    {
        if (!IsAuthenticated) return;

        _superadminUserInputViewModel = _serviceProvider.GetRequiredService<SuperadminUserInputViewModel>();
        _superadminUserInputViewModel.GoBackAction = async () => await NavigateToUserDetail(userId);
        _superadminUserInputViewModel.NavigateToUserDetail = NavigateToUserDetail;
        await _superadminUserInputViewModel.InitializeAsync(userId); // Load existing user

        CurrentView = _superadminUserInputViewModel;
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

    private async Task<UserTenantListViewModel> GetTenantListViewModelAsync()
    {
        if (_tenantListViewModel == null)
        {
            _tenantListViewModel = _serviceProvider.GetRequiredService<UserTenantListViewModel>();
            _tenantListViewModel.NavigateToTenantDetail = NavigateToUserTenantDetail;
            _tenantListViewModel.NavigateToEditTenant = NavigateToEditUserTenant;
            _tenantListViewModel.NavigateToCreateTenant = NavigateToCreateUserTenant;
            await _tenantListViewModel.InitializeAsync();
        }
        return _tenantListViewModel;
    }

    private async Task<UserTenantListViewModel> GetTenantListWithRefreshAsync()
    {
        var listViewModel = await GetTenantListViewModelAsync();
        await listViewModel.RefreshAsync();
        return listViewModel;
    }

    public async Task NavigateToUserTenantDetail(Guid tenantId)
    {
        if (!IsAuthenticated) return;

        _tenantDetailViewModel = _serviceProvider.GetRequiredService<UserTenantDetailViewModel>();
        _tenantDetailViewModel.GoBackAction = async () => await NavigateToUserTenantList();
        _tenantDetailViewModel.NavigateToTenantInput = NavigateToEditUserTenant;
        await _tenantDetailViewModel.InitializeAsync(tenantId);

        CurrentView = _tenantDetailViewModel;
        SelectedMenuItem = "MyTenantDetail";
    }

    public async Task NavigateToEditUserTenant(Guid tenantId)
    {
        if (!IsAuthenticated) return;

        _tenantInputViewModel = _serviceProvider.GetRequiredService<UserTenantInputViewModel>();
        _tenantInputViewModel.GoBackAction = tenantId != Guid.Empty
            ? async () => await NavigateToUserTenantDetail(tenantId)
            : async () => await NavigateToUserTenantList();
        await _tenantInputViewModel.InitializeAsync(tenantId);

        CurrentView = _tenantInputViewModel;
        SelectedMenuItem = "MyTenantInput";
    }

    public async Task NavigateToCreateUserTenant()
    {
        if (!IsAuthenticated) return;

        _tenantInputViewModel = _serviceProvider.GetRequiredService<UserTenantInputViewModel>();
        _tenantInputViewModel.GoBackAction = async () => await NavigateToUserTenantList();
        await _tenantInputViewModel.InitializeAsync(Guid.Empty);

        CurrentView = _tenantInputViewModel;
        SelectedMenuItem = "MyTenantInput";
    }

    [RelayCommand]
    private async Task NavigateToUserTenantList()
    {
        var listViewModel = await GetTenantListViewModelAsync();
        await listViewModel.RefreshAsync();
        CurrentView = listViewModel;
        SelectedMenuItem = "MyTenants";
    }

    public async Task NavigateToManufacturerDetail(Guid manufacturerId)
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
        await _manufacturerInputViewModel.InitializeAsync(Guid.Empty);
        
        CurrentView = _manufacturerInputViewModel;
        SelectedMenuItem = "ManufacturerInput";
    }

    public async Task NavigateToEditManufacturer(Guid manufacturerId)
    {
        if (!IsAuthenticated) return;

        _manufacturerInputViewModel = _serviceProvider.GetRequiredService<ManufacturerInputViewModel>();
        _manufacturerInputViewModel.GoBackAction = async () => await NavigateToManufacturerDetail(manufacturerId);
        _manufacturerInputViewModel.NavigateToManufacturerDetail = NavigateToManufacturerDetail;
        await _manufacturerInputViewModel.InitializeAsync(manufacturerId); // Load existing manufacturer
        
        CurrentView = _manufacturerInputViewModel;
        SelectedMenuItem = "ManufacturerInput";
    }

    private async void OnTenantCreated()
    {
        _debugService.LogInfo("Tenant created successfully - reloading user session");

        // Logout and navigate back to login to refresh tenant list
        await LogoutAsync();

        // Optionally, show a success message or automatically re-login
        CurrentView = LoginViewModel;
    }

    private async void OnTenantSetupLogout()
    {
        _debugService.LogInfo("User logged out from tenant setup");
        await LogoutAsync();
    }

    [RelayCommand]
    public void ToggleDebugWindow()
    {
        _debugService.ToggleDebugWindow();
        _debugService.LogInfo("Debug window toggled via F12 key");
    }

    [RelayCommand]
    public void ToggleTheme()
    {
        IsDarkTheme = !IsDarkTheme;

        // TODO: Re-implement theme switching for Uno Platform
        // Use Microsoft.UI.Xaml.Application.Current.RequestedTheme = ElementTheme.Dark/Light
        _debugService.LogWarning($"Theme toggle not yet implemented for Uno Platform. Requested: {(IsDarkTheme ? "Dark" : "Light")}");
    }

    private void InitializeTheme()
    {
        // TODO: Re-implement theme detection for Uno Platform
        // Use Microsoft.UI.Xaml.Application.Current.RequestedTheme
        IsDarkTheme = false; // Default to light theme for now

        _debugService.LogInfo($"Theme initialized to: {(IsDarkTheme ? "Dark" : "Light")} (Uno Platform theme detection not yet implemented)");
    }
}
