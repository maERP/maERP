using maERP.Domain.Dtos.User;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public string userId { get; set; } = "";

    // ReSharper disable once NotAccessedField.Local
    private MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected UserDetailDto User = new();

    protected override async Task OnParametersSetAsync()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (userId != null)
        {
            Title = "bearbeiten";
            User = await HttpService.GetAsync<UserDetailDto>($"/api/v1/Users/{userId}") ?? new UserDetailDto();
        }
    }

    protected async Task OnValidSubmit()
    {
        await HttpService.PutAsJsonAsync($"/api/v1/Users/{userId}", User);
        NavigateToList();
    }

    public void NavigateToList()
    {
        NavigationManager.NavigateTo("/Users");
    }
}