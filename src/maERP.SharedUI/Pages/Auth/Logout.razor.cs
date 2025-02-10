using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Auth;

public partial class Logout
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        HttpService.Logout();
        NavigationManager.NavigateTo("/login");
        await Task.CompletedTask;
    }
}