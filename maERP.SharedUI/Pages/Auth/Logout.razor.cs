namespace maERP.SharedUI.Pages.Auth;

public partial class Logout
{

    protected override async Task OnInitializedAsync()
    {
        await _authService.LogoutAsync();
        _navigationManager.NavigateTo("/login");
    }
}