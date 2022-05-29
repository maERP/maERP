namespace maERP.Client.Contracts.Services
{
    public interface INavigationService
    {
        Task NavigateToPage<T>(object parameter = null) where T : Page;
        Task NavigateBack();
        Task NavigateToPageModal<T>() where T : Page;
    }
}
