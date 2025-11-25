using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Helpers;
using maERP.Client.Features.Auth.Services;
using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.Shell.Models;

namespace maERP.Client.Features.Auth.Models;

public class LoginModel : INotifyPropertyChanged
{
    private readonly IDispatcher _dispatcher;
    private readonly INavigator _navigator;
    private readonly IAuthenticationService _authentication;
    private readonly IMaErpAuthenticationService _maErpAuth;
    private readonly ShellModel _shell;
    private readonly IHostEnvironment _hostEnvironment;

    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _serverUrl = "https://";
    private string _errorMessage = string.Empty;
    private bool _isLoading;

    public LoginModel(
        IDispatcher dispatcher,
        INavigator navigator,
        IAuthenticationService authentication,
        IMaErpAuthenticationService maErpAuth,
        ShellModel shell,
        IHostEnvironment hostEnvironment)
    {
        Console.WriteLine("====== LoginModel Constructor CALLED ======");
        _dispatcher = dispatcher;
        _navigator = navigator;
        _authentication = authentication;
        _maErpAuth = maErpAuth;
        _shell = shell;
        _hostEnvironment = hostEnvironment;

        // Initialize values based on environment
        var isDevelopment = _hostEnvironment.IsDevelopment();
        Console.WriteLine($"====== IsDevelopment: {isDevelopment} ======");

        if (isDevelopment)
        {
            _email = "admin@localhost.com";
            _password = "P@ssword1";
            _serverUrl = "https://localhost:8443";
        }

        LoginCommand = new RelayCommand(async () => await LoginAsync(), () => CanLogin);

        Console.WriteLine("====== LoginModel initialized ======");
    }

    public string Title { get; } = "Login";

    public string Email
    {
        get => _email;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged();
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public string ServerUrl
    {
        get => _serverUrl;
        set
        {
            if (_serverUrl != value)
            {
                _serverUrl = value;
                OnPropertyChanged();
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (_errorMessage != value)
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading != value)
            {
                _isLoading = value;
                OnPropertyChanged();
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public bool CanLogin =>
        !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(Password) &&
        !string.IsNullOrWhiteSpace(ServerUrl) &&
        !IsLoading;

    public ICommand LoginCommand { get; }

    private async Task LoginAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var email = Email;
            var password = Password;
            var serverUrl = ServerUrl;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(serverUrl))
            {
                ErrorMessage = "Please fill in all fields";
                return;
            }

            // Normalize server URL
            if (!serverUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !serverUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                serverUrl = "https://" + serverUrl;
            }

            // Remove trailing slash
            serverUrl = serverUrl.TrimEnd('/');

            var credentials = new Dictionary<string, string>
            {
                ["Email"] = email,
                ["Password"] = password,
                ["ServerUrl"] = serverUrl
            };

            var success = await _authentication.LoginAsync(_dispatcher, credentials);

            if (success)
            {
                _shell.UpdateAuthenticationState(true);
                await _navigator.NavigateRouteAsync(this, Routes.Dashboard, qualifier: Qualifiers.ClearBackStack);
            }
            else
            {
                ErrorMessage = "Login failed. Please check your credentials and server URL.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
