using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Auth;

public partial class Logout
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAuthenticationService AuthService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await AuthService.LogoutAsync();
        NavigationManager.NavigateTo("/login");
    }
}