using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.User;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.Administration.ViewModels;

public partial class UserListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<UserListDto> users = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowDataGrid))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowDataGrid))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private int totalCount;

    [ObservableProperty]
    private int currentPage;

    [ObservableProperty]
    private int pageSize = 50;

    [ObservableProperty]
    private UserListDto? selectedUser;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<string, Task>? NavigateToUserDetail { get; set; }
    public Func<Task>? NavigateToCreateUser { get; set; }

    public UserListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadUsersAsync();
    }

    [RelayCommand]
    private async Task LoadUsersAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<UserListDto>("users", CurrentPage, PageSize, SearchText, "Lastname Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Users.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Users.Clear();
                foreach (var user in result.Data)
                {
                    Users.Add(user);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {Users.Count} users successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Benutzer";
                Users.Clear();
                _debugService.LogError($"Failed to load users: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Benutzer: {ex.Message}";
            Users.Clear();
            _debugService.LogError($"Exception loading users: {ex}");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("LoadUsersAsync completed");
        }
    }

    [RelayCommand]
    private async Task SearchUsersAsync()
    {
        CurrentPage = 0;
        await LoadUsersAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadUsersAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadUsersAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadUsersAsync();
        }
    }

    [RelayCommand]
    private void SelectUser(UserListDto? user)
    {
        SelectedUser = user;
    }

    [RelayCommand]
    private async Task ViewUserDetails(UserListDto? user)
    {
        if (user == null || NavigateToUserDetail == null) return;
        
        SelectedUser = user;
        await NavigateToUserDetail(user.Id);
    }

    [RelayCommand]
    private async Task CreateNewUser()
    {
        if (NavigateToCreateUser == null) return;
        
        await NavigateToCreateUser();
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}