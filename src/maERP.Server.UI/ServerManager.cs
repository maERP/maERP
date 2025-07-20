using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace maERP.Server.UI;

public static class ServerManager
{
    private static Process? _serverProcess;

    public static string GetServerExecutableName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "maERP.Server.exe";
        }
        
        return "maERP.Server";
    }
    
    /// <summary>
    /// Startet den maERP.Server als separaten Prozess
    /// </summary>
    /// <param name="port">Port auf dem der Server laufen soll (default: 5000)</param>
    /// <param name="databaseProvider">Datenbankprovider (MySQL, PostgreSQL, MSSQL, SQLite)</param>
    /// <param name="connectionString">Connection String für die Datenbank</param>
    /// <returns>True wenn der Server erfolgreich gestartet wurde</returns>
    public static async Task<bool> StartServerAsync(int port = 5000, string? databaseProvider = null, string? connectionString = null)
    {
        try
        {
            // Pfad zum Server-Binary finden
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var serverPath = Path.Combine(appDirectory, "Server", GetServerExecutableName());
            
            if (!File.Exists(serverPath))
            {
                // Alternative: Suche im Unterverzeichnis
                serverPath = Path.Combine(appDirectory, "maERP.Server", GetServerExecutableName());
                
                if (!File.Exists(serverPath))
                {
                    Console.WriteLine($"Server executable not found at: {serverPath}");
                    return false;
                }
            }
            
            // Server-Prozess konfigurieren
            var startInfo = new ProcessStartInfo
            {
                FileName = serverPath,
                Arguments = $"--urls=http://localhost:{port}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(serverPath)
            };
            
            // Umgebungsvariablen setzen
            startInfo.EnvironmentVariables["ASPNETCORE_ENVIRONMENT"] = "Production";
            startInfo.EnvironmentVariables["ASPNETCORE_URLS"] = $"http://localhost:{port}";
            
            // Datenbankeinstellungen als Umgebungsvariablen setzen
            if (!string.IsNullOrEmpty(databaseProvider))
            {
                startInfo.EnvironmentVariables["DatabaseConfig__Provider"] = databaseProvider;
            }
            
            if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(databaseProvider))
            {
                startInfo.EnvironmentVariables[$"DatabaseConfig__ConnectionStrings__{databaseProvider}"] = connectionString;
            }
            
            _serverProcess = Process.Start(startInfo);
            
            if (_serverProcess == null)
            {
                Console.WriteLine("Failed to start server process");
                return false;
            }
            
            // Warten bis Server bereit ist
            await Task.Delay(2000);
            
            if (_serverProcess.HasExited)
            {
                Console.WriteLine($"Server process exited prematurely with code: {_serverProcess.ExitCode}");
                return false;
            }
            
            Console.WriteLine($"maERP.Server started successfully on port {port}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting server: {ex.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Stoppt den Server-Prozess
    /// </summary>
    public static void StopServer()
    {
        try
        {
            if (_serverProcess != null && !_serverProcess.HasExited)
            {
                _serverProcess.Kill();
                _serverProcess.WaitForExit(5000);
                Console.WriteLine("maERP.Server stopped");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error stopping server: {ex.Message}");
        }
        finally
        {
            _serverProcess?.Dispose();
            _serverProcess = null;
        }
    }
    
    /// <summary>
    /// Prüft ob der Server läuft
    /// </summary>
    public static bool IsServerRunning()
    {
        return _serverProcess != null && !_serverProcess.HasExited;
    }
    
    /// <summary>
    /// Ermittelt die Server-URL
    /// </summary>
    public static string GetServerUrl(int port = 5000)
    {
        return $"http://localhost:{port}";
    }
}