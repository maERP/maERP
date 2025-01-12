using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Auth;

public partial class Logout
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IAuthenticationService _authService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await _authService.LogoutAsync();
        _navigationManager.NavigateTo("/login");
    }
}