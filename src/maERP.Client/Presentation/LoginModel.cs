using maERP.Domain.Dtos.Auth;
using maERP.Client.Services.Authentication;

namespace maERP.Client.Presentation;

public partial record LoginModel
{
    private readonly IDispatcher _dispatcher;
    private readonly INavigator _navigator;
    private readonly IAuthenticationService _authentication;
    private readonly IMaErpAuthenticationService _maErpAuth;
    private readonly ShellModel _shell;
    private readonly IHostEnvironment _hostEnvironment;

    // IState backing fields - MVUX pattern requires these to be stored, not computed
    private readonly IState<string> _email;
    private readonly IState<string> _password;
    private readonly IState<string> _serverUrl;

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

        // Initialize State values based on environment
        var isDevelopment = _hostEnvironment.IsDevelopment();
        Console.WriteLine($"====== IsDevelopment: {isDevelopment} ======");

        _email = State<string>.Value(this, () => isDevelopment ? "admin@localhost.com" : string.Empty);
        _password = State<string>.Value(this, () => isDevelopment ? "P@ssword1" : string.Empty);
        _serverUrl = State<string>.Value(this, () => isDevelopment ? "https://localhost:8443" : "https://");

        Console.WriteLine("====== LoginModel States initialized ======");
    }

    public string Title { get; } = "Login";

    // Public IState properties for XAML binding
    public IState<string> Email => _email;
    public IState<string> Password => _password;
    public IState<string> ServerUrl => _serverUrl;
    public IState<string> ErrorMessage => State<string>.Value(this, () => string.Empty);
    public IState<bool> IsLoading => State<bool>.Value(this, () => false);
    public IState<bool> HasError => State<bool>.Async(this, async ct => !string.IsNullOrEmpty(await ErrorMessage));
    public IState<bool> CanLogin => State<bool>.Async(this, async ct =>
    {
        var email = await Email;
        var password = await Password;
        var serverUrl = await ServerUrl;
        var isLoading = await IsLoading;

        return !string.IsNullOrWhiteSpace(email) &&
               !string.IsNullOrWhiteSpace(password) &&
               !string.IsNullOrWhiteSpace(serverUrl) &&
               !isLoading;
    });

    public async ValueTask Login(CancellationToken token)
    {
        await IsLoading.UpdateAsync(_ => true, token);
        await ErrorMessage.UpdateAsync(_ => string.Empty, token);

        try
        {
            var email = await Email;
            var password = await Password;
            var serverUrl = await ServerUrl;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(serverUrl))
            {
                await ErrorMessage.UpdateAsync(_ => "Please fill in all fields", token);
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

            var success = await _authentication.LoginAsync(_dispatcher, credentials, cancellationToken: token);

            if (success)
            {
                _shell.UpdateAuthenticationState(true);
                await _navigator.NavigateViewModelAsync<MainModel>(this, qualifier: Qualifiers.ClearBackStack);
            }
            else
            {
                await ErrorMessage.UpdateAsync(_ => "Login failed. Please check your credentials and server URL.", token);
            }
        }
        catch (Exception ex)
        {
            await ErrorMessage.UpdateAsync(_ => $"An error occurred: {ex.Message}", token);
        }
        finally
        {
            await IsLoading.UpdateAsync(_ => false, token);
        }
    }
}
