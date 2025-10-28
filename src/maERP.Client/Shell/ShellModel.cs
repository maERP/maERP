namespace maERP.Client.Shell;

public class ShellModel
{
    private readonly INavigator _navigator;

    public ShellModel(
        IAuthenticationService authentication,
        INavigator navigator)
    {
        _navigator = navigator;
        _authentication = authentication;
        _authentication.LoggedOut += LoggedOut;
    }

    private async void LoggedOut(object? sender, EventArgs e)
    {
        await _navigator.NavigateViewModelAsync<Features.Authentication.Models.LoginModel>(this, qualifier: Qualifiers.ClearBackStack);
    }

    private readonly IAuthenticationService _authentication;
}
