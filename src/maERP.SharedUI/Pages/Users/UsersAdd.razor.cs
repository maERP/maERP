using maERP.Domain.Dtos.User;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersAdd
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public string userId { get; set; } = "";

    protected string Title = "hinzufügen";

    protected UserDetailDto User = new();

    protected async Task Save()
    {
        await HttpService.PostAsync<UserDetailDto, UserDetailDto>("/api/v1/users", User);
        ReturnToList();
    }

    public void ReturnToList()
    {
        NavigationManager.NavigateTo("/users");
    }
}