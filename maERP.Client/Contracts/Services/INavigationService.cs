namespace maERP.Client.Contracts.Services
{
    public interface INavigationService
    {
        Task NavigateToSecondPage(string id);
        Task NavigateToThirdPage();
        Task NavigateBack();
        Task NavigateToMainPage();
        Task NavigateToLogin();
    }
}
