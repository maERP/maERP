using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.User;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class UserListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

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

    public UserListViewModel(IHttpService httpService)
    {
        _httpService = httpService;
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
                System.Diagnostics.Debug.WriteLine("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Users.Clear();
                foreach (var user in result.Data)
                {
                    Users.Add(user);
                }
                TotalCount = result.TotalCount;
                System.Diagnostics.Debug.WriteLine($"Loaded {Users.Count} users successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Benutzer";
                Users.Clear();
                System.Diagnostics.Debug.WriteLine($"Failed to load users: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Benutzer: {ex.Message}";
            Users.Clear();
            System.Diagnostics.Debug.WriteLine($"Exception loading users: {ex}");
        }
        finally
        {
            IsLoading = false;
            System.Diagnostics.Debug.WriteLine($"finally");
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

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}