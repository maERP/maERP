using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersAdd
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IUserService UserService { get; set; }

    [Parameter]
    public string userId { get; set; } = "";

    protected string Title = "hinzuf�gen";

    protected UserVm User = new();

    protected async Task Save()
    {
        await UserService.CreateUser(User);
        ReturnToList();
    }

    public void ReturnToList()
    {
        NavigationManager.NavigateTo("/users");
    }
}