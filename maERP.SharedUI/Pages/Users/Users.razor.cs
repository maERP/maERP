using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class Users
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IUserService _userService { get; set; }

    private ICollection<UserVM>? users;

    protected override async Task OnInitializedAsync()
    {
        users = await _userService.GetUsers();
    }
}