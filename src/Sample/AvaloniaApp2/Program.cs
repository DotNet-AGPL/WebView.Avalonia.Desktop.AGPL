using Avalonia;
using Microsoft.Extensions.Logging;
using WebView.Avalonia.Core.Tools;
using WebView.Avalonia.Desktop;

namespace AvaloniaApp2;

internal class Program
{
    private static ILogger logger = LoggerFactoryTool.GetLogger<Program>();

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) 
    {
        try 
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);
            logger.LogInformation(ex.StackTrace);
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseWinJitWebView2();
}
