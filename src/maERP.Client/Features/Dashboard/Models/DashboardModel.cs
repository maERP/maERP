using maERP.Client.Core.Constants;
using maERP.Client.Models;

namespace maERP.Client.Features.Dashboard.Models;

public partial record DashboardModel
{
    private readonly INavigator _navigator;
    private readonly IAuthenticationService _authentication;

    public DashboardModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        IAuthenticationService authentication,
        INavigator navigator)
    {
        _navigator = navigator;
        _authentication = authentication;
        Title = "Dashboard";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public async Task GoToSecond()
    {
        var name = await Name;
        await _navigator.NavigateRouteAsync(this, Routes.Second, data: new Entity(name!));
    }

    public async ValueTask Logout(CancellationToken token)
    {
        await _authentication.LogoutAsync(token);
    }
}
