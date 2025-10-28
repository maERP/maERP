namespace maERP.Client.Features.Authentication.Models;

public partial record LoginModel(IDispatcher Dispatcher, INavigator Navigator, IAuthenticationService Authentication)
{
    public string Title { get; } = "Login";


    public async ValueTask Login(CancellationToken token)
    {
        var success = await Authentication.LoginAsync(Dispatcher);
        if (success)
        {
            await Navigator.NavigateViewModelAsync<Features.Dashboard.Models.DashboardModel>(this, qualifier: Qualifiers.ClearBackStack);
        }
    }
}
