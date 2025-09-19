using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using System.Linq;

#if WINDOWS
using System.ServiceProcess;
using System.Security.Principal;
#endif

namespace maERP.Server.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private const string ServiceName = "maERP.Server";

    public MainWindowViewModel()
    {
        PropertyChanged += OnPropertyChanged;
        UpdateWindowsServiceSectionVisibility();
        UpdateServiceStatus();
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

    [ObservableProperty]
    private string _serviceStatus = "Windows-Service nicht eingerichtet";

    [ObservableProperty]
    private string _serviceButtonText = "Windows-Service einrichten";

    [ObservableProperty]
    private bool _isServiceInstalled = false;

    [ObservableProperty]
    private string _serviceStatusBackground = "#FFF2F2";

    [ObservableProperty]
    private string _serviceStatusBorderBrush = "#FFD0D0";

    [ObservableProperty]
    private string _serviceStatusForeground = "#CC3333";

    [ObservableProperty]
    private bool _isWindowsServiceSectionVisible = false;

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

    private void UpdateWindowsServiceSectionVisibility()
    {
        IsWindowsServiceSectionVisible = OperatingSystem.IsWindows() && !System.Diagnostics.Debugger.IsAttached;
    }

    private void UpdateServiceStatus()
    {
        try
        {
            if (OperatingSystem.IsWindows())
            {
#if WINDOWS
                var service = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == ServiceName);
                IsServiceInstalled = service != null;

                if (IsServiceInstalled)
                {
                    ServiceStatus = service.Status switch
                    {
                        ServiceControllerStatus.Running => "Windows-Service läuft",
                        ServiceControllerStatus.Stopped => "Windows-Service gestoppt",
                        ServiceControllerStatus.StartPending => "Windows-Service wird gestartet...",
                        ServiceControllerStatus.StopPending => "Windows-Service wird gestoppt...",
                        _ => "Windows-Service Status unbekannt"
                    };
                    ServiceButtonText = "Windows-Service entfernen";
                    ServiceStatusBackground = "#F0F8FF";
                    ServiceStatusBorderBrush = "#D0E7FF";
                    ServiceStatusForeground = "#0066CC";
                }
                else
                {
                    ServiceStatus = "Windows-Service nicht eingerichtet";
                    ServiceButtonText = "Windows-Service einrichten";
                    ServiceStatusBackground = "#FFF2F2";
                    ServiceStatusBorderBrush = "#FFD0D0";
                    ServiceStatusForeground = "#CC3333";
                }
#else
                ServiceStatus = "Windows-Service nicht verfügbar (plattformspezifisch)";
                ServiceButtonText = "Nicht verfügbar";
                IsServiceInstalled = false;
                ServiceStatusBackground = "#FFFACD";
                ServiceStatusBorderBrush = "#F0E68C";
                ServiceStatusForeground = "#B8860B";
#endif
            }
            else
            {
                ServiceStatus = "Nur auf Windows verfügbar";
                ServiceButtonText = "Nicht verfügbar";
                ServiceStatusBackground = "#FFFACD";
                ServiceStatusBorderBrush = "#F0E68C";
                ServiceStatusForeground = "#B8860B";
            }
        }
        catch
        {
            ServiceStatus = "Fehler beim Prüfen des Service-Status";
            ServiceButtonText = "Windows-Service einrichten";
            IsServiceInstalled = false;
            ServiceStatusBackground = "#FFF2F2";
            ServiceStatusBorderBrush = "#FFD0D0";
            ServiceStatusForeground = "#CC3333";
        }
    }

    [RelayCommand]
    private async Task ToggleWindowsService()
    {
        if (!OperatingSystem.IsWindows())
        {
            ServiceStatus = "Windows-Service ist nur auf Windows verfügbar";
            return;
        }

        if (!IsRunAsAdministrator())
        {
            ServiceStatus = "Administratorrechte erforderlich";
            await Task.Delay(2000);
            UpdateServiceStatus();
            return;
        }

        try
        {
            if (IsServiceInstalled)
            {
                await UninstallService();
            }
            else
            {
                await InstallService();
            }
        }
        catch (Exception ex)
        {
            ServiceStatus = $"Fehler: {ex.Message}";
            await Task.Delay(3000);
            UpdateServiceStatus();
        }
    }

    private static bool IsRunAsAdministrator()
    {
        if (!OperatingSystem.IsWindows())
            return false;

#if WINDOWS
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
#else
        return false;
#endif
    }

    private async Task InstallService()
    {
        ServiceStatus = "Windows-Service wird installiert...";

        var executablePath = Process.GetCurrentProcess().MainModule?.FileName;
        if (string.IsNullOrEmpty(executablePath))
        {
            throw new InvalidOperationException("Konnte Executable-Pfad nicht ermitteln");
        }

        var serverExePath = executablePath.Replace("maERP.Server.UI.exe", "maERP.Server.exe");

        var startInfo = new ProcessStartInfo
        {
            FileName = "sc.exe",
            Arguments = $"create \"{ServiceName}\" binPath= \"{serverExePath}\" start= auto DisplayName= \"maERP Server\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using var process = Process.Start(startInfo);
        if (process != null)
        {
            await process.WaitForExitAsync();

            if (process.ExitCode == 0)
            {
                ServiceStatus = "Windows-Service erfolgreich installiert";
            }
            else
            {
                var error = await process.StandardError.ReadToEndAsync();
                throw new InvalidOperationException($"Installation fehlgeschlagen: {error}");
            }
        }

        await Task.Delay(1000);
        UpdateServiceStatus();
    }

    private async Task UninstallService()
    {
        ServiceStatus = "Windows-Service wird entfernt...";

        var startInfo = new ProcessStartInfo
        {
            FileName = "sc.exe",
            Arguments = $"delete \"{ServiceName}\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using var process = Process.Start(startInfo);
        if (process != null)
        {
            await process.WaitForExitAsync();

            if (process.ExitCode == 0)
            {
                ServiceStatus = "Windows-Service erfolgreich entfernt";
            }
            else
            {
                var error = await process.StandardError.ReadToEndAsync();
                throw new InvalidOperationException($"Deinstallation fehlgeschlagen: {error}");
            }
        }

        await Task.Delay(1000);
        UpdateServiceStatus();
    }
}