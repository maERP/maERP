using maERP.SharedUI.Models.User;

namespace maERP.SharedUI.Pages.Users;

public partial class Users
{

    private ICollection<UserVM>? users;

    protected override async Task OnInitializedAsync()
    {
        users = await _userService.GetUsers();
    }
}