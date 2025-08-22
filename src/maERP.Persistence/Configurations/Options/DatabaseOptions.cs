namespace maERP.Persistence.Configurations.Options;

public class DatabaseOptions
{
    public const string Section = "DatabaseConfig";

    public string Provider { get; set; } = "MySQL";
    public string ConnectionString { get; set; } = string.Empty;
    public Dictionary<string, string> ConnectionStringExamples { get; set; } = new();

    public string GetConnectionString() => string.IsNullOrEmpty(ConnectionString)
        ? throw new InvalidOperationException("No connection string configured")
        : ConnectionString;
}