using maERP.Shared.Contracts;
using maERP.Shared.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace maERP.Shared.Pages;

public partial class Index
{
    [Inject]
    private NavigationManager? NavManager { get; set; }

    [Inject]
    private AuthenticationStateProvider? _authenticationStateProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ((ApiAuthenticationStateProvider)_authenticationStateProvider!).GetAuthenticationStateAsync();

        if (result.User != null)
        {
            NavManager!.NavigateTo("/dashboard");
        }
        else
        {
            NavManager!.NavigateTo("/login");
        }
    }
}