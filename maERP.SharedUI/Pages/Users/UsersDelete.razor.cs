using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersDelete
{

    [Parameter]
    public string? userId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (userId != null)
        {
            // await authManagerRepository.DeleteByIdAsync(userId);
            NavigationManager.NavigateTo("/users");
        }

        await Task.CompletedTask;
    }
}