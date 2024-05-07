using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersAdd
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IUserService _userService { get; set; }

    [Parameter]
    public string userId { get; set; } = "";

    protected string Title = "hinzufügen";

    protected UserVM user = new();

    protected async Task Save()
    {
        await _userService.CreateUser(user);
        ReturnToList();
    }

    public void ReturnToList()
    {
        _navigationManager.NavigateTo("/users");
    }
}