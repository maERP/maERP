using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class Users
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IUserService UserService { get; set; }

    private ICollection<UserVm>? _users;

    protected override async Task OnInitializedAsync()
    {
        _users = await UserService.GetUsers();
    }
}