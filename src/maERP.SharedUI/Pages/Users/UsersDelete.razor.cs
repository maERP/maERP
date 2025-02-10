using System.Net;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersDelete
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    
    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public string? userId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (userId != null)
        {
            await HttpService.DeleteAsync($"/api/v1/Users/{userId}");
            NavigationManager.NavigateTo("/Users");
        }
    }
}