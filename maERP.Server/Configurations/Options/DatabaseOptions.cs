namespace maERP.Server.Configurations.Options;

public class DatabaseOptions
{
    public const string Section = "ConnectionStrings";

    public string DefaultConnection { get; set; } = string.Empty;
}