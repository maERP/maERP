using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace maERP.Server.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SelectedDatabaseProvider))
        {
            UpdateDefaultDatabasePort();
        }
    }

    private void UpdateDefaultDatabasePort()
    {
        DatabasePort = SelectedDatabaseProvider switch
        {
            "MySQL" => "3306",
            "PostgreSQL" => "5432",
            "MSSQL" => "1433",
            "SQLite" => "",
            _ => DatabasePort
        };
    }

    private string GenerateConnectionString()
    {
        return SelectedDatabaseProvider switch
        {
            "MySQL" => $"Server={DatabaseHost};Port={DatabasePort};Database={DatabaseName};Uid={DatabaseUsername};Pwd=;",
            "PostgreSQL" => $"Host={DatabaseHost};Port={DatabasePort};Database={DatabaseName};Username={DatabaseUsername};Password=;",
            "MSSQL" => $"Server={DatabaseHost},{DatabasePort};Database={DatabaseName};User Id={DatabaseUsername};Password=;TrustServerCertificate=True;",
            "SQLite" => $"Data Source={DatabaseName}.db",
            _ => ""
        };
    }
    [ObservableProperty]
    private string _serverStatus = "Status: maERP.Server gestoppt";

    [ObservableProperty]
    private string _serverButtonText = "maERP.Server starten";

    [ObservableProperty]
    private bool _isServerRunning = false;

    [ObservableProperty]
    private string _serverPort = "5000";

    [ObservableProperty]
    private string _selectedDatabaseProvider = "MySQL";

    [ObservableProperty]
    private string _databaseHost = "localhost";

    [ObservableProperty]
    private string _databasePort = "3306";

    [ObservableProperty]
    private string _databaseUsername = "maerp";

    [ObservableProperty]
    private string _databaseName = "maerp_01";

    public ObservableCollection<string> DatabaseProviders { get; } = new()
    {
        "MySQL", "PostgreSQL", "MSSQL", "SQLite"
    };

    [RelayCommand]
    private async Task ToggleServer()
    {
        if (IsServerRunning)
        {
            await StopServer();
        }
        else
        {
            await StartServer();
        }
    }

    private async Task StartServer()
    {
        try
        {
            var connectionString = GenerateConnectionString();
            var port = int.Parse(ServerPort);
            
            var success = await ServerManager.StartServerAsync(port, SelectedDatabaseProvider, connectionString);
            
            if (success)
            {
                IsServerRunning = true;
                ServerStatus = $"Status: maERP.Server gestartet (Port {ServerPort})";
                ServerButtonText = "maERP.Server stoppen";
            }
            else
            {
                ServerStatus = "Status: Fehler beim Starten des Servers";
            }
        }
        catch
        {
            ServerStatus = "Status: Fehler beim Starten des Servers";
        }
    }

    private async Task StopServer()
    {
        try
        {
            ServerManager.StopServer();
            
            IsServerRunning = false;
            ServerStatus = "Status: maERP.Server gestoppt";
            ServerButtonText = "maERP.Server starten";
            
            await Task.Delay(500);
        }
        catch
        {
            ServerStatus = "Status: Fehler beim Stoppen des Servers";
        }
    }
}