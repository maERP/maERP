using CommunityToolkit.Mvvm.ComponentModel;

namespace maERP.UI.Shared.ViewModels;

public partial class ConfirmationDialogViewModel : ViewModelBase
{
    [ObservableProperty]
    private string title = "Bestätigung";

    [ObservableProperty]
    private string message = "Sind Sie sicher?";

    [ObservableProperty]
    private string confirmButtonText = "Ja";

    [ObservableProperty]
    private string cancelButtonText = "Abbrechen";

    [ObservableProperty]
    private string icon = "❓";

    public void Initialize(string dialogTitle, string dialogMessage, string confirmText = "Ja", string cancelText = "Abbrechen", string dialogIcon = "❓")
    {
        Title = dialogTitle;
        Message = dialogMessage;
        ConfirmButtonText = confirmText;
        CancelButtonText = cancelText;
        Icon = dialogIcon;
    }
}