using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace maERP.UI.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private string welcomeMessage = "Willkommen bei maERP";

    [ObservableProperty]
    private DateTime currentDate = DateTime.Now;

    public DashboardViewModel()
    {
        // Hier können später Dashboard-spezifische Daten geladen werden
    }
}