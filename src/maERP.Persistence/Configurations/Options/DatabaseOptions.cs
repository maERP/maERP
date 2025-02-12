namespace maERP.Persistence.Configurations.Options;

public class DatabaseOptions
{
    public const string Section = "DatabaseConfig";

    public string Provider { get; set; } = "MySQL";
    public Dictionary<string, string> ConnectionStrings { get; set; } = new();

    public string GetConnectionString() => ConnectionStrings.GetValueOrDefault(Provider) ?? 
        throw new InvalidOperationException($"No connection string found for provider {Provider}");
}