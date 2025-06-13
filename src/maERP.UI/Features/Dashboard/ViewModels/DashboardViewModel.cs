using System;
using CommunityToolkit.Mvvm.ComponentModel;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Dashboard.ViewModels;

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