using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersDetail
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IUserService _userService { get; set; }

    [Parameter]
    public string userId { get; set; } = "";

    protected string Title = "Benutzerdetail";

    protected UserVM user = new();

    protected override async Task OnParametersSetAsync()
    {
        if (userId != null)
        {
            user = await _userService.GetUserDetails(userId);
        }
    }
}