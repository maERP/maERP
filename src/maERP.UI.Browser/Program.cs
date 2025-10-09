using System.Runtime.Versioning;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Browser;

using maERP.UI;

internal sealed partial class Program
{
    private static Task Main(string[] args) => BuildAvaloniaApp()
            .WithInterFont()
            .StartBrowserAppAsync(
                "out",
                new BrowserPlatformOptions
                {
                    RenderingMode = new[] { BrowserRenderingMode.WebGL1 }
                });

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}
