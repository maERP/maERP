using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersDelete
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Parameter]
    public string? userId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (userId != null)
        {
            // await authManagerRepository.DeleteByIdAsync(userId);
            _navigationManager.NavigateTo("/users");
        }

        await Task.CompletedTask;
    }
}