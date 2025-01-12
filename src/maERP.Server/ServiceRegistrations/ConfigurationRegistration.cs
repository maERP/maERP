using System.Reflection;

namespace maERP.Server.ServiceRegistrations;

public static class ConfigurationRegistration
{
    public static string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;
    public static IConfiguration AppSetting { get; }

    static ConfigurationRegistration()
    {
        AppSetting = new ConfigurationBuilder()
           .SetBasePath(currentPath)
           .AddJsonFile("Settings/appsettings.json")
           .Build();
    }
}